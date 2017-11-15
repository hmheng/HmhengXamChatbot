﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HmhengXamChatbot.Core.Models
{
    class Message
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public TextAlignment LblMessageHorizontalTextAlignment { get; set; }
        public TextAlignment LblSenderHorizontalTextAlignment { get; set; }

        public Message(string text, string sender)
        {
            Text = text;
            Sender = sender;
            if (Sender.ToUpper() == "ME")
            {
                LblMessageHorizontalTextAlignment = TextAlignment.End;
                LblSenderHorizontalTextAlignment = TextAlignment.End;
            }
            else
            {
                LblMessageHorizontalTextAlignment = TextAlignment.Start;
                LblSenderHorizontalTextAlignment = TextAlignment.Start;
            }
        }
    }
}
