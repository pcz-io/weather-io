#!/bin/bash

function check_password {
    [[ "$1" =~ ^[a-zA-Z0-9]{10,128}$ ]] && [[ "$1" =~ ^.*[a-z].*$ ]] && [[ "$1" =~ ^.*[A-Z].*$ ]] && [[ "$1" =~ ^.*[0-9].*$ ]] \
        && return 0 \
        || return 1
}

function delete_images {
    docker stop weatherio-server weatherio-web weatherio-db
    docker rm weatherio-server weatherio-web
    docker image rm weatherioweb weatherioserver
}

function display_help {
    echo "usage: ./run-compose.sh [--help] [--update]"
    echo ""
    echo "  --help     displays this message"
    echo "  --update   removes images and compiles backend again"
    echo "  --detach   runs docker compose in detached mode"
    exit
}

while [[ $# -gt 0 ]]; do
  case "$1" in
    --help)
      display_help
      ;;
    --update)
      update=1
      ;;
    --detach)
      detach=1
      ;;
    *)
      echo "error: Unknown option '$1'"
      display_help
      ;;
  esac
  shift
done

if [ -f ".env" ]; then
    readarray -t lines < .env
    for line in "${lines[@]}"; do
        var="${line%=*}"
        val="${line#*=}"
        if [[ "$var" == "CERT_PASSWD" ]]; then
            cert_password=$val
        elif [[ "$var" == "DB_PASSWD" ]]; then
            db_password=$val
        fi
    done
fi

while ! check_password $db_password; do
    read -p "Enter Database password: " -s db_password
    echo
done

while ! check_password $cert_password; do
    read -p "Enter Certificate password: " -s cert_password
    echo
done

echo \
"CERT_PASSWD=$cert_password
DB_PASSWD=$db_password" > .env

if [ "$update" = 1 ]; then
    echo "Deleting images..."
    delete_images
fi

additional_args=""

if [ "$detach" = 1 ]; then
    echo "Running in detached mode..."
    additional_args="$additional_args --detach"
fi

docker compose -f docker-compose-relase.yml up $additional_args
