from diagrams import Cluster, Diagram, Edge
from diagrams.custom import Custom
from diagrams.onprem.compute import Server
from diagrams.azure.compute import FunctionApps

img_watson_ui = "watson-ui.png"
img_weather = "hg-weather.png"

with Diagram("Chatbot - Messenger", show=False, direction="TB"):
    watson_ui = Custom("Watson UI", img_watson_ui)
    chatbot_api = FunctionApps("API Chatbot")
    weather = Custom("HG weather", img_weather)

    watson_ui >> Edge(color="Orange", label=" /webhook") >> chatbot_api >> weather
