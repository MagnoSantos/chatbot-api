from diagrams import Cluster, Diagram, Edge
from diagrams.custom import Custom
from diagrams.azure.integration import ServiceBus
from diagrams.onprem.compute import Server
from diagrams.azure.compute import FunctionApps

img_messenger_path = "messenger.png"
img_watson_path = "watson-assistant.png"
img_graph_api_path = "graph-api.jpg"

with Diagram("Chatbot - Messenger", show=False, direction="TB"):
    messenger = Custom("Facebook Messenger", img_messenger_path)
    servicebus = ServiceBus("ServiceBus Messenger")
    chatbot_api = Server("API Chatbot")
    function = FunctionApps("Function Webhook")
    watson_assistant = Custom("Watson Assistant", img_watson_path)
    graph_api = Custom("Graph API", img_graph_api_path)

    messenger >> Edge(color="Orange", label=" /webhook") >> function >> Edge(color="Orange") >> servicebus
    chatbot_api << Edge(color="Orange") << servicebus 
    chatbot_api >> Edge(color="Blue") >>watson_assistant 
    chatbot_api >> Edge(color="Blue") >>graph_api
