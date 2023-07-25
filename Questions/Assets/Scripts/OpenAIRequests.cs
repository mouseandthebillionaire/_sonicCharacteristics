/* https://www.immersivelimit.com/tutorials/how-to-use-chatgpt-in-unity */

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Collections.Generic;
using CandyCoded.env;
using UnityEngine;

public class OpenAIRequests : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> messages;

    public static OpenAIRequests S;

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // This line gets your API key
        if (env.TryParseEnvironmentVariable("OPENAI_API_KEY", out string ak))
        {
            api = new OpenAIAPI(ak);
            StartConversation();
        }
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, "You are an inquisitive robot who is asking questions of a human participant. They do not fully trust you, but you want to be as friendly as possible to win their trust.")
        };
    }

    public async void GetOpenAIResponse(string messageToSend)
    {
        // Fill the user message from the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = messageToSend;
            // Add the message to the list
        messages.Add(userMessage);

        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.3,
            MaxTokens = 50,
            Messages = messages
        });

        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        // Add the response to the list of messages
        messages.Add(responseMessage);

        // Add them to the GlobalVariable list of messages from the OpenAI System
        GlobalVariables.S.openAIMessages.Add(string.Format(responseMessage.Content));
    }
}