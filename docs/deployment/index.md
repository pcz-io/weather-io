# Deployment guide for administrators

Prerequirements:

- docker

To begin with you need to have docker installed on the machine. A guide on docker installation can be found at: [https://docs.docker.com/engine/install/](https://docs.docker.com/engine/install/). Next you need to clone the repository with command:

```bash
git clone https://github.com/pcz-io/weather-io.git
```

This will download repository with application code.

## Running server

The easiest way to start server is to run `run-compose.sh` script located in `WeatherIO/` folder.
So to do that change working directory to `WeatherIO` and run script by typing commands:

```bash
cd WeatherIO
sudo ./run-compose.sh --detach
```

It is necessary to add `--detach` argument to run docker compose in detached mode, otherwise it would not run in background.
If your user is not in `docker` group you need to run script in privilaged mode e.g. by running with `sudo`.

## Help message

To show a script help message you can run:

```bash
./run-compose.sh --help
```

## Updating application

To update te application you need to pull new version from the remote repository and run `run-compose.sh` with additional `--update` argument.

```bash
git pull
sudo ./run-compose.sh --update --detach
```

By adding `--update`, script will stop application containers, delete them (database will be preserved), remove old images, build new version and start all necessary containers.
