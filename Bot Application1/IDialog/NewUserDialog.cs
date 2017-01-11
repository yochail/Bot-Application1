﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Bot_Application1.Cardatt_achment;
using Bot_Application1.dataBase;
using NLPtest;
using Bot_Application1.YAndex;
using NLPtest.WorldObj;
using Model.dataBase;
using static Bot_Application1.Controllers.ConversationController;

namespace Bot_Application1.IDialog
{
    [Serializable]
    public class NewUserDialog : AbsDialog
    {
     
        public override async Task StartAsync(IDialogContext context)
        {

            context.UserData.TryGetValue<Users >("user", out user);
            if (user == null)
            {
                user = new Users();
                context.UserData.SetValue<Users >("user", user);
            }
            context.Wait(this.NewUser);
        }



        public async virtual Task NewUser(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            
            var message = await result;

            var newMessage = conv().getPhrase(Pkey.selfIntroduction);
            await writeMessageToUser(context, newMessage);
            await NewUserGetName(context, result);
            //user Name

        }

        public async virtual Task NewUserGetName(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
         



            
            context.UserData.TryGetValue<Users >("user", out user);
            if (user.UserName == "" || user.UserName == null)
            {
                if (context.Activity.ChannelId == "facebook")
                {
                    var userFBname = context.Activity.From.Name;
                    var userTranslation = ControlerTranslate.Translate(userFBname);


                    if(userTranslation != "")
                    {
                        user.UserName = userTranslation.Split(' ')[0];
                        context.UserData.SetValue<Users >("user", user);
                        await NewUserGetName(context, result);
                    }
                }
                else
                {
                await writeMessageToUser(context, conv().getPhrase(Pkey.NewUserGetName));
                context.Wait(CheckName);

                } 

            }
            else
            {
                await writeMessageToUser(context, conv().getPhrase(Pkey.NewUserGreeting));
                await NewUserGetGender(context, result);
            }
        }

        public async virtual Task CheckName(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            
            var message = await result;
            var userText = await result;

        
            if ((user.UserName = conv().getName(userText.Text)) != null)
            {
                var newMessage = conv().getPhrase(Pkey.NewUserGreeting);

                context.UserData.SetValue<Users >("user", user);

                 await writeMessageToUser(context, newMessage);
                 await NewUserGetGender(context, result);
  
            }
            else
            {
                var newMessage = conv().getPhrase(Pkey.MissingUserInfo,textVar:"name");
                await writeMessageToUser(context, newMessage);
                context.Wait(CheckName);
            }
        }


        public async virtual Task NewUserGetGender(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //user Name
            
            context.UserData.TryGetValue<Users >("user", out user);
            //user Gender
            if (user.UserGender == "" || user.UserGender == null)
            {
             

                var menu = new PromptDialog.PromptChoice<string>(
                 conv().getGenderOptions(),
                conv().getPhrase(Pkey.NewUserGetGender)[0],
                conv().getPhrase(Pkey.wrongOption)[0],
                3);

                context.Call(menu, CheckGender);

            }else
            {
                await NewUserGetClass(context, result);
            }
        }

        public async virtual Task CheckGender(IDialogContext context, IAwaitable<object> result)
        {
            
            var userText = await result as string;

         
            if ((user.UserGender = conv().getGender(userText)) != null)
            {
                context.UserData.SetValue<Users >("user", user);

                await writeMessageToUser(context, conv().getPhrase(Pkey.GenderAck, textVar: userText));
        
             
                await NewUserGetClass(context, null);

            }
            else
            {
                var newMessage = conv().getPhrase(Pkey.MissingUserInfo,textVar:"gender");
                await writeMessageToUser(context, newMessage);
                context.Wait(CheckGender);
            }
        }

        public async virtual Task NewUserGetClass(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            context.UserData.TryGetValue<Users >("user", out user);
 

            if (user.UserClass == "" || user.UserClass == null)
            {
                var menu = new PromptDialog.PromptChoice<string>(
                 conv().getClassOptions(),
                conv().getPhrase(Pkey.NewUserGetClass)[0],
                conv().getPhrase(Pkey.wrongOption)[0],
                3);

                context.Call(menu, CheckClass);

            }
            else
            {
                await LetsStart(context, result);
            }

        }



        public async virtual Task CheckClass(IDialogContext context, IAwaitable<string> result)
        {
            //user class
            var userText = await result;
            
            if ((user.UserClass = conv().getClass(userText)) != null)
            {
                context.UserData.SetValue<Users >("user", user);

                await writeMessageToUser(context, conv().getPhrase(Pkey.GeneralAck,textVar:user.UserClass));


                await LetsStart(context, null);

            }
            else
            {

                await writeMessageToUser(context, conv().getPhrase(Pkey.MissingUserInfo, textVar: "class"));
                context.Wait<string>(CheckClass);
            }

        }


        public async virtual Task LetsStart(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //user class
            
            await writeMessageToUser(context, conv().getPhrase(Pkey.LetsStart));
            context.Done("");
        }

    }
}