﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


using System.Threading;
using NLP;
using NLP.WorldObj;
using Model.dataBase;
using Model;
using Model.Models;
using Bot_Application1.Models;

namespace Bot_Application1.IDialog
{
    [Serializable]
    public class NotImplamentedDialog : AbsDialog<IMessageActivity>
    {

      
        public override async Task StartAsync(IDialogContext context)
        {
            getDialogsVars(context);
         

            await context.PostAsync(conv().getPhrase(Pkey.NotImplamented)[0]);
            context.Done("");

        }

        public override UserContext getDialogContext()
        {
            base.getDialogContext();
            UserContext.dialog = "NotImplamentedDialog";
            return UserContext;
        }



    }
}