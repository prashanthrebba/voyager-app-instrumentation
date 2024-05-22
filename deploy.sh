DOCKER_USERNAME="rebbaprashanth"
DOCKER_PASSWORD="dckr_pat_o_bIlAELweln9veJKAuoa2vdy2I"

HELM_CHART_DIR="helm_chart"
HELM_RELEASE_NAME="voyager"
NAMESPACE="voyager-app"
LATEST_IMAGE_TAG=$(git rev-parse HEAD~0^{commit})
SERVICE_NAME="voyager-api"

HOST_PORT=5000
CONTAINER_PORT=80


IMAGE_NAME="$DOCKER_USERNAME/demo-app-relic"

echo "Logging in to Docker Hub..."
docker login -u "$DOCKER_USERNAME" -p $DOCKER_PASSWORD
echo "" 

echo "Latest image tag retrieved: $LATEST_IMAGE_TAG"
echo ""

echo "Building Docker image..."
docker buildx build -f src/Voyager.Api/Dockerfile -t "$IMAGE_NAME:$LATEST_IMAGE_TAG" .
echo "Docker image build completed."
echo ""

echo "Pushing Docker image..."
docker push "$IMAGE_NAME:$LATEST_IMAGE_TAG"
echo "Docker image pushed successfully."
echo ""

echo "Upgrading Helm release..."
helm upgrade --install "$HELM_RELEASE_NAME" -f "$HELM_CHART_DIR/values.yaml" --set image.tag=$LATEST_IMAGE_TAG $HELM_CHART_DIR --namespace=$NAMESPACE
echo ""

echo "Checking deployment status..."
kubectl rollout status deployment/$SERVICE_NAME -n $NAMESPACE
echo ""

echo "Successfully image built, pushed, and deployed the application!"


echo ""
echo "application listening on $HOST_PORT"
kubectl port-forward deployment/$SERVICE_NAME $HOST_PORT:$CONTAINER_PORT -n $NAMESPACE
