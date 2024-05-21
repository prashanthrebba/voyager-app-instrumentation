# Install the Kubernetes integration

## Run this command on your host to install Kubernetes integration.

    KSM_IMAGE_VERSION="v2.10.0" && helm repo add newrelic https://helm-charts.newrelic.com && helm repo update && kubectl create namespace newrelic ; helm upgrade --install newrelic-bundle newrelic/nri-bundle --set global.licenseKey=52c4fb704d10ecc86d42a8b080d61c13FFFFNRAL --set global.cluster=voyager --namespace=newrelic --set newrelic-infrastructure.privileged=true --set global.lowDataMode=true --set kube-state-metrics.image.tag=${KSM_IMAGE_VERSION} --set kube-state-metrics.enabled=true --set kubeEvents.enabled=true --set newrelic-prometheus-agent.enabled=true --set newrelic-prometheus-agent.lowDataMode=true --set newrelic-prometheus-agent.config.kubernetes.integrations_filter.enabled=false --set logging.enabled=true --set newrelic-logging.lowDataMode=true

## Command to upgrade the newrelic-bundle helm release using the values file.

    helm repo add newrelic https://helm-charts.newrelic.com && helm repo update && kubectl create namespace newrelic ; helm upgrade --install newrelic-bundle newrelic/nri-bundle -n newrelic --values values.yaml
