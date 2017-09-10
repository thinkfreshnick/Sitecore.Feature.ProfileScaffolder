using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using System.Collections.Specialized;

namespace Sitecore.Feature.ProfileScaffolder.Shell.Framework.Commands
{
    public class ScaffoldProfileKey : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            if (context.Items.Length == 1 && context.Items[0] != null)
            {
                var item = context.Items[0];

                var nameValueCollection = new NameValueCollection()
                {
                    { "database", item.Database.Name },
                    { "id", item.ID.ToString() },
                    { "language", item.Language.ToString() }
                };

                Context.ClientPage.Start("uiScaffoldProfileKey", nameValueCollection);
            }
        }

        /// <summary>
        /// Queries the state of the command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The state of the command.</returns>
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            if (context.Items.Length != 1)
                return CommandState.Disabled;

            var item = context.Items[0];

            if (item.Appearance.ReadOnly)
                return CommandState.Disabled;

            // profile keys can only be created under profile items

            if (item.TemplateID.ToString() != "{8E0C1738-3591-4C60-8151-54ABCC9807D1}") // profile template
                return CommandState.Hidden;

            return base.QueryState(context);
        }
    }   
}