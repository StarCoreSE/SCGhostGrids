using System;
using System.Collections.Generic;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRageMath;


namespace klime.GhostGrids
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class GhostGrids : MySessionComponentBase
    {
        public override void Init(MyObjectBuilder_SessionComponent sessionComponent)
        {
            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
        }

        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
        {
            if (messageText == "/ghost")
            {
                var charac = MyAPIGateway.Session.Player.Character;
                BoundingSphereD sphere = new BoundingSphereD(charac.GetPosition(), 20000);

                List<MyEntity> entities = new List<MyEntity>();
                MyGamePruningStructure.GetAllTopMostEntitiesInSphere(ref sphere, entities);

                foreach (var entity in entities)
                {
                    MyCubeGrid grid = entity as MyCubeGrid;
                    if (grid != null)
                    {
                        if (grid.DisplayName == "blocker")
                        {
                            grid.Physics.Enabled = false;
                            MyAPIGateway.Utilities.ShowMessage("", $"Ghosted: {grid.DisplayName}");
                        }
                    }
                }
            }
        }

        public override void UpdateAfterSimulation()
        {

        }
        protected override void UnloadData()
        {
            MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
        }
    }
}