using FakeMyResume.Services.Interfaces;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;

namespace FakeMyResume.Services;

public class TextService(IOpenAIService openAiService) : ITextService
{
    public async Task<string?> GetSuggestions(string text)
    {
        var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages =
            [
                ChatMessage.FromSystem("You are an expert assistant in correcting the grammar and spelling of provided texts."),
                ChatMessage.FromUser($"Suggest two options to improve the following text, make it a bit more professional and/or fix grammar: {text}"),
            ],
            Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo
        });
        if (completionResult.Successful)
        {
            var content = completionResult.Choices.First().Message.Content;
            return content;
        }
        else
        {
            return null;
        }
    }
}
