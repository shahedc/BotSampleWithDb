// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBotWithDb.Data;
using EchoBotWithDb.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.EntityFrameworkCore;

namespace EchoBotWithDb.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly ApplicationDbContext _context;

        public EchoBot(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var humanInfo = string.Empty;

            // OPTION 1: get human data
            //var firstHuman = await _context.Human.FirstOrDefaultAsync();

            // OPTION 2: get human data with SQL query
            var firstHuman = await _context.Human
                .FromSql("SELECT * FROM dbo.Human")
                .FirstOrDefaultAsync();

            if (firstHuman != null)
                humanInfo = firstHuman.Name + ": " + firstHuman.Title;

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Human Info: {humanInfo}!"), cancellationToken);
                }
            }
        }
    }
}
