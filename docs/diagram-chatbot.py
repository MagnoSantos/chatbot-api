from diagrams import Cluster, Diagram, Edge
from diagrams.custom import Custom
from diagrams.azure.integration import ServiceBus
from diagrams.onprem.compute import Server
from diagrams.azure.compute import FunctionApps

img_watson_ui = "watson-ui.png"
img_watson_path = "watson-assistant.png"
img_weather = "hg-weather.png"

with Diagram("Chatbot - Messenger", show=False, direction="TB"):
    watson_ui = Custom("Watson UI", img_watson_ui)
    servicebus = ServiceBus("ServiceBus Messenger")
    chatbot_api = Server("API Chatbot")
    watson_assistant = Custom("Watson Assistant", img_watson_path)
    weather = Custom("HG weather", img_weather)

    watson_ui >> Edge(
        color="Orange", label=" /webhook") >> chatbot_api >> Edge(color="Orange") >> servicebus
    chatbot_api << Edge(color="Blue") << servicebus
    chatbot_api >> Edge(color="Blue") >> watson_assistant
    chatbot_api >> Edge(color="Blue") >> weather
