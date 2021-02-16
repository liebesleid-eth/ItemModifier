using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Reflection;

namespace ItemModifier
{
    public class ItemModifier : RocketPlugin<ItemModifierConfiguration>
    {
        protected override void Load()
        {
            if (Level.isLoaded)
                LoadItemModifications(0);
            else
                Level.onPostLevelLoaded += LoadItemModifications;

            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Level.onPostLevelLoaded -= LoadItemModifications;

            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        private void LoadItemModifications(int level)
        {
            foreach (var item in Configuration.Instance.Items)
            {
                object asset = Assets.find(EAssetType.ITEM, item.Id);
                if (asset == null)
                {
                    Logger.LogWarning($"Asset for item Id {item.Id} not found");
                    continue;
                }
                    
                foreach (var modification in item.Modifications)
                {
                    var field = asset.GetType().GetField(modification.Name);
                    var property = asset.GetType().GetProperty(modification.Name);
                    
                    try
                    {
                        if (field != null)
                        {
                            
                            field.SetValue(asset, Convert.ChangeType(modification.Value, field.FieldType));
                        }
                        else if (property != null)
                        {
                            property.SetValue(asset, Convert.ChangeType(modification.Value, property.PropertyType));
                        } else
                        {
                            Logger.LogWarning($"Failed to find member {modification.Name} for asset Id {item.Id} type {asset.GetType().Name}");
                            continue;
                        }

                        Logger.Log($"Successfully set modification {modification.Name} value to {modification.Value} for item id {item.Id}");
                    }
                    catch (Exception e)
                    {
                        Logger.LogException(e);
                    }
                }
            }
        }
    }
}
