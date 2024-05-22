# Voyager

## Description

1. Created a sample .NET Core application called `voyager-app` for demonstration purposes.
2. Wrote a Dockerfile to build and containerize the `voyager-app` application.
3. Set up a local Kubernetes (k8s) cluster on the development machine for testing and deployment.
4. Deployed the containerized `voyager-app` to the local k8s cluster.

## k8s Setup on Local Machine

- Followed the official Minikube documentation at [https://minikube.sigs.k8s.io/docs/start/](https://minikube.sigs.k8s.io/docs/start/) to set up a local Kubernetes cluster using Minikube.
- On macOS, used the command `brew install minikube` to install Minikube, a tool that runs a single-node Kubernetes cluster inside a Virtual Machine (VM) on the local machine.
- Executed `minikube start` to launch the local Kubernetes cluster.
- Set up the New Relic Kubernetes monitoring by executing a specific command to integrate New Relic with the local k8s cluster.
  - This command sets up the necessary components and configurations for monitoring the local Kubernetes cluster using the New Relic platform.

================================================================================

## New Relic Configuration

### Integrated New Relic to Monitor the k8s Cluster

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

### Resources

- [kubernetes-integration-install-configure](https://docs.newrelic.com/docs/kubernetes-pixie/kubernetes-integration/installation/kubernetes-integration-install-configure/)

Note: The license key (`52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL`) used in the above command is specific to the New Relic account used for this project. Replace it with your own New Relic license key.

### Integrated New Relic to Monitor/Observe the `voyager-app` Application

- Integrated the OpenTelemetry SDK into the `voyager-app` .NET Core application to enable telemetry data collection and observability.
- Configured New Relic to receive and process the telemetry data pushed from the OpenTelemetry SDK instrumented in the `voyager-app`.
  - This allows monitoring and observing the `voyager-app` application's performance, metrics, and other telemetry data within the New Relic platform.

================================================================================

## Deployment on Local Kubernetes Cluster

- Created a `deploy.sh` script file to automate the deployment process of the `voyager-app` to the local Kubernetes cluster.
- The `deploy.sh` script performs the following steps:
  1. Builds the Dockerfile to create a Docker image for the `voyager-app` with the tag set as the latest commit ID from the version control system (e.g., Git).
  2. Pushes the Docker image to a container registry (in this case, Docker Hub) for easy access and distribution.
  3. Deploys the `voyager-app` to the local Kubernetes cluster using Helm, a package manager for Kubernetes, with the image name and tag created using the latest commit ID.
- After successful deployment, the `voyager-app` is accessible on `http://localhost:5000/WeatherForecast` (assuming the application exposes an endpoint at `/WeatherForecast`).

By following these steps, the `voyager-app` is containerized, integrated with OpenTelemetry for observability, and deployed to a local Kubernetes cluster for testing and development purposes. Additionally, the New Relic platform is configured to monitor and observe the Kubernetes cluster and the `voyager-app` application running within it.
