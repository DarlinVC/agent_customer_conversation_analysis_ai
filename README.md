
# Agent - Customer Conversation Analysis

This project leverages the GPT API to analyze conversations between agents and customers. Its goal is to enhance the communication skills of agents and provide insights into the customer's experience throughout the interaction. The REST API can be used in real time during a chat or after the conversation has ended.

# JSON Structure
{
  "conversation": [
    // Interactions
    { 
      "agent": "",
      "customer": ""
    },
    {
      "agent": "",
      "customer": ""
    }
  ]
}

# JSON RESPONSE 
{
  "conversation": [
    {
      "agent": "",
      "customer": "",
      "analysis": {
        "errorsAgent": "",
        "CustomerFeelAndUnderstanding": "",
        "tipsAgentImprove": ""
      }
    },
    {
      "agent": "",
      "customer": "",
      "analysis": {
        "errorsAgent": "",
        "CustomerFeelAndUnderstanding": "",
        "tipsAgentImprove": ""
      }
    }
  ],
  "overallSatisfaction": ""
}



