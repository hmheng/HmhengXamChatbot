using HmhengXamChatbot.Core.Models;
using HmhengXamChatbot.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HmhengXamChatbot
{
    public partial class MainPage : ContentPage
    {
        //Initialize a connection with ID and Name
        BotConnection connection = new BotConnection("ME");

        //ObservableCollection to store the messages to be displayed
        ObservableCollection<Message> messageList = new ObservableCollection<Message>();

        public MainPage()
        {
            InitializeComponent();

            //Bind the ListView to the ObservableCollection
            MessageListView.ItemsSource = messageList;

            //Start listening to messages
            var messageTask = connection.GetMessagesAsync(messageList);

        }

        //Send method for message entry
        public void Send(object sender, EventArgs args)
        {
            //Get text in entry
            var message = ((Entry)sender).Text;

            if (message.Length > 0)
            {
                //Clear entry
                ((Entry)sender).Text = "";

                //Make object to be placed in ListView
                var messageListItem = new Message(message, connection.Account.Name);
                messageList.Add(messageListItem);

                //Send the message to the bot
                connection.SendMessage(message);
            }
        }

    }
}
