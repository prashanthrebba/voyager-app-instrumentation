# Voyager

## Description

Steps involved in setting up and deploying the `voyager-app` application to a local Kubernetes cluster, along with integrating it with New Relic for monitoring and observability.

# Voyager Application Setup and Deployment

## 1. Application Overview

- The `voyager-app` is a sample .NET Core application created for demonstration purposes.
- It exposes the following endpoints:
  - `GET /api/v1/WeatherForecast`
  - `GET /api/v1/Game`
  - `GET /api/v1/Joke`
  - `GET /api/v1/Recipe`

## 2. Containerization with Docker

- A Dockerfile was created to build and containerize the `voyager-app` application.
- The Dockerfile specifies the instructions for creating a Docker image for the application.

## 3. Local Kubernetes (k8s) Cluster Setup

- A local Kubernetes cluster was set up on the development machine using Minikube for testing and deployment purposes.
- Minikube is a tool that runs a single-node Kubernetes cluster inside a Virtual Machine (VM) on the local machine.
- On macOS, Minikube was installed using the command `brew install minikube`.
- The local Kubernetes cluster was launched by executing `minikube start`.

## 4. New Relic Integration

### 4.1. Monitoring the Local Kubernetes Cluster

- New Relic was integrated with the local Kubernetes cluster to enable monitoring and observability.
- The following command was executed to set up the necessary components and configurations for monitoring the local cluster using the New Relic platform:

```bash
KSM_IMAGE_VERSION="v2.10.0" && \
helm repo add newrelic https://helm-charts.newrelic.com && \
helm repo update && \
kubectl create namespace newrelic && \
helm upgrade --install newrelic-bundle newrelic/nri-bundle \
  --set global.licenseKey=52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL \
  --set global.cluster=voyager \
  --namespace=newrelic \
  --set newrelic-infrastructure.privileged=true \
  --set global.lowDataMode=true \
  --set kube-state-metrics.image.tag=${KSM_IMAGE_VERSION} \
  --set kube-state-metrics.enabled=true \
  --set kubeEvents.enabled=true \
  --set newrelic-prometheus-agent.enabled=true \
  --set newrelic-prometheus-agent.lowDataMode=true \
  --set newrelic-prometheus-agent.config.kubernetes.integrations_filter.enabled=false \
  --set logging.enabled=true \
  --set newrelic-logging.lowDataMode=true
```

**Note**: The license key (`52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL`) used in the command is specific to the project's New Relic account. Replace it with your own New Relic license key.

### 4.2. Monitoring the `voyager-app` Application

- The OpenTelemetry SDK was integrated into the `voyager-app` .NET Core application to enable telemetry data collection and observability.
- New Relic was configured to receive and process the telemetry data pushed from the OpenTelemetry SDK instrumented in the `voyager-app`.
- This setup allows monitoring and observing the `voyager-app` application's performance, metrics, and other telemetry data within the New Relic platform.

## 5. Deployment on Local Kubernetes Cluster

- A `deploy.sh` script file was created to automate the deployment process of the `voyager-app` to the local Kubernetes cluster [as mentioned here](deploy.sh)
- The `deploy.sh` script performs the following steps:
  1. Builds the Dockerfile to create a Docker image for the `voyager-app` with the tag set as the latest commit ID from the version control system (e.g., Git).
  2. Pushes the Docker image to a container registry (in this case, Docker Hub) for easy access and distribution [as mentioned here](src/Voyager.Api/Dockerfile)
  3. Deploys the `voyager-app` to the local Kubernetes cluster using Helm, a package manager for Kubernetes, with the image name and tag created using the latest commit ID.
- After successful deployment, the `voyager-app` is accessible on `http://localhost:5000/api/v1/WeatherForecast`.

## 6. Accessing the Application

Once the deployment is completed, the `voyager-app` application can be accessed locally at the following URLs:

- `http://localhost:5000/api/v1/WeatherForecast`
- `http://localhost:5000/api/v1/Game`
- `http://localhost:5000/api/v1/Joke`
- `http://localhost:5000/api/v1/Recipe`

These endpoints correspond to the various functionalities provided by the `voyager-app`.

## 7. Monitoring and Observability with New Relic

With the integration of New Relic, you can monitor and observe the following:

- The local Kubernetes cluster itself, including its components and resources.
- The `voyager-app` application running within the local Kubernetes cluster, including its performance, metrics, and other telemetry data.

By leveraging the New Relic platform, we can gain insights into the health, performance, and overall behavior of both the Kubernetes cluster and the deployed application, enabling effective monitoring and troubleshooting.

This setup provides a development environment for testing and experimenting with containerized applications deployed to a local Kubernetes cluster, while also incorporating monitoring and observability capabilities through New Relic integration.
