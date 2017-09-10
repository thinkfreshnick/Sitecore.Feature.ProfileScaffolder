using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Pipelines;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Feature.ProfileScaffolder.Shell.Framework.Pipelines
{
    public class ScaffoldProfileKey : ItemOperation
    {
        public virtual void GetProfileKeyName(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!args.IsPostBack)
            {
                Context.ClientPage.ClientResponse.Input("Enter a new name for the profile key:", "", Settings.ItemNameValidation, "'$Input' is not a valid name.", Settings.MaxItemNameLength, "Profile Key Name");

                args.WaitForPostBack();

                return;
            }
            if (args.Result == null || args.Result.Length == 0 || args.Result == "null" || args.Result == "undefined")
            {
                args.AbortPipeline();

                return;
            }

            if (args.Result.Trim().Length == 0)
            {
                Context.ClientPage.ClientResponse.Alert("The name cannot be blank.");
                Context.ClientPage.ClientResponse.Input("Enter a new name for the profile key:", string.Empty, Settings.ItemNameValidation, "'$Input' is not a valid name.", Settings.MaxItemNameLength, "Profile Key Name");

                args.WaitForPostBack();

                return;
            }

            Context.ClientPage.ServerProperties["Name"] = args.Result;

            args.IsPostBack = false;

            return;
        }

        public virtual void ExecuteScaffolding(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            var db = Database.GetDatabase(args.Parameters["database"]);
            var language = Language.Parse(args.Parameters["language"]);
            var profile = db.GetItem(args.Parameters["id"], language);

            var name = StringUtil.GetString(Context.ClientPage.ServerProperties["Name"]);

            var profileKey = profile.Children[name] ?? profile.Add(name, new TemplateID(ID.Parse(ProfileScaffolderSettings.ProfileKeyTemplateId)));

            profileKey.Editing.BeginEdit();

            profileKey[ProfileScaffolderSettings.ProfileKeyFieldNames.MinValue] = ProfileScaffolderSettings.ProfileKeyDefaultMinValue;
            profileKey[ProfileScaffolderSettings.ProfileKeyFieldNames.MaxValue] = ProfileScaffolderSettings.ProfileKeyDefaultMaxValue;

            profileKey.Editing.EndEdit();

            ExecuteProfileCardScaffolding(profile, profileKey);
            ExecutePatternCardScaffolding(profile, profileKey);
        }

        public virtual void ExecuteProfileCardScaffolding(Item profile, Item key)
        {
            var folder = profile.Children[ProfileScaffolderSettings.ProfileCardFolderName];

            var card = folder.Children[key.Name] ?? folder.Add(key.Name, new TemplateID(ID.Parse(ProfileScaffolderSettings.ProfileCardTemplateId)));

            card.Editing.BeginEdit();

            card[ProfileScaffolderSettings.ProfileCardFieldNames.RadarGraph] = $"<tracking><profile id=\"{profile.ID}\" name=\"{profile.Name}\"><key name=\"{key.Name}\" value=\"10\" /></profile></tracking>";

            card.Editing.EndEdit();
        }

        public virtual void ExecutePatternCardScaffolding(Item profile, Item key)
        {
            var folder = profile.Children[ProfileScaffolderSettings.PatternCardFolderName];

            var card = folder.Children[key.Name] ?? folder.Add(key.Name, new TemplateID(ID.Parse(ProfileScaffolderSettings.PatternCardTemplateId)));

            card.Editing.BeginEdit();

            card[ProfileScaffolderSettings.PatternCardFieldNames.RadarGraph] = $"<tracking><profile id=\"{profile.ID}\" name=\"{profile.Name}\"><key name=\"{key.Name}\" value=\"10\" /></profile></tracking>";

            card.Editing.EndEdit();
        }
    }
}