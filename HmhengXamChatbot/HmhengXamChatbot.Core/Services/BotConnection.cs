using HmhengXamChatbot.Core.Models;
using Microsoft.Bot.Connector.DirectLine;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HmhengXamChatbot.Core.Services
{
    class BotConnection
    {
        public string botId = "hmhengbotdemo";
        public string directLineSecret = "<YOUR_DIRECTLINE_SECRET_HERE>";
        public DirectLineClient directLineClient;
        public Conversation MainConversation;
        public ChannelAccount Account;

        public BotConnection(string accountName)
        {
            // Obtain a token using the Direct Line secret
            var tokenResponse = new DirectLineClient(directLineSecret).Tokens.GenerateTokenForNewConversation();

            // Use token to create conversation
            directLineClient = new DirectLineClient(tokenResponse.Token);
            MainConversation = directLineClient.Conversations.StartConversation();
            Account = new ChannelAccount { Id = accountName, Name = accountName };
        }

        public void SendMessage(string message)
        {
            Activity activity = new Activity
            {
                From = Account,
                Text = message,
                Type = ActivityTypes.Message
            };
            directLineClient.Conversations.PostActivity(MainConversation.ConversationId, activity);
        }

        public async Task GetMessagesAsync(ObservableCollection<Message> collection)
        {
            string watermark = null;

            while (true)
            {
                Debug.WriteLine("Reading message every 3 seconds");

                var activitySet = await directLineClient.Conversations.GetActivitiesAsync(MainConversation.ConversationId, watermark);
                watermark = activitySet?.Watermark;

                foreach (Activity activity in activitySet.Activities)
                {
                    if (activity.From.Id == botId)
                    {
                        collection.Add(new Message(activity.Text, activity.From.Name));
                    }
                }

                await Task.Delay(3000);
            }
        }


    }
}
