
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace FiaKaiera.VRCSharedSpacesExample
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpawnByPlayerData : UdonSharpBehaviour
    {
        /*

            The following example teleports the LocalPlayer's location to a specified Transform
            if the specified key and value pair is present and matching within the LocalPlayer's PlayerData
            after the LocalPlayer has loaded their PlayerData in-world.
            
            An event to save the player's spawn location to the Transform "SaveLocation()" is included,
            but not modifying or removing it.

            Even though the following example is usable on its own, unless you only plan to detect one user,
            do not use this as there are better ways of teleporting multiple users to their destinations.

        */

        // From vrc-shared-spaces by fiaKaiera
        // https://github.com/fiaKaiera/vrc-shared-spaces (MIT license)

        VRCPlayerApi localPlayer;
        [SerializeField] string persistenceKey = "LocationSaved";
        [SerializeField] string persistenceValue = "Yes";
        [SerializeField] Transform spawnLocation;

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer) return;
            localPlayer = Networking.LocalPlayer;

            if (PlayerData.TryGetString(player, persistenceKey, out string value)) {
                if (value == persistenceValue)
                    SendCustomEvent(nameof(_TeleportLocalPlayer));
            }
        }

        public void _TeleportLocalPlayer()
        {
            if (!Utilities.IsValid(localPlayer)) return;
            localPlayer.TeleportTo(spawnLocation.position, spawnLocation.rotation, VRC_SceneDescriptor.SpawnOrientation.Default, false);
        }

        // Call this as an event or function to save the player's location
        public void SaveLocation() =>
            PlayerData.SetString(persistenceKey, persistenceValue);
    }
}