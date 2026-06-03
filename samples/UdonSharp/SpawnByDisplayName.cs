
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace FiaKaiera.VRCSharedSpacesExample
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpawnByDisplayName : UdonSharpBehaviour
    {
        /*

            The following example teleports the LocalPlayer's location to a specified Transform
            if the specified displayName matches 10 frames after the player has loaded in-world.

            Even though the following example is usable on its own, unless you only plan to detect one user,
            do not use this as there are better ways of teleporting multiple users to their destinations.

        */

        // From vrc-shared-spaces by fiaKaiera
        // https://github.com/fiaKaiera/vrc-shared-spaces (MIT license)

        const int FRAME_DELAY = 10;

        VRCPlayerApi localPlayer;
        [SerializeField] string playerDisplayName = "DatOneMFInParticular";
        [SerializeField] Transform spawnLocation;

        void Start()
        {
            localPlayer = Networking.LocalPlayer;
            if (localPlayer.displayName == playerDisplayName)
                SendCustomEventDelayedFrames(nameof(TeleportLocalPlayer), FRAME_DELAY);
        }

        public void TeleportLocalPlayer() =>
            localPlayer.TeleportTo(spawnLocation.position, spawnLocation.rotation, VRC.SDKBase.VRC_SceneDescriptor.SpawnOrientation.Default, false);
    }    
}
