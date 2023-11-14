#!/bin/bash

function check_password {
    [[ "$1" =~ ^[a-zA-Z0-9]{10,128}$ ]] && [[ "$1" =~ ^.*[a-z].*$ ]] && [[ "$1" =~ ^.*[A-Z].*$ ]] && [[ "$1" =~ ^.*[0-9].*$ ]] \
        && return 0 \
        || return 1
}

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

docker compose -f docker-compose-relase.yml up
