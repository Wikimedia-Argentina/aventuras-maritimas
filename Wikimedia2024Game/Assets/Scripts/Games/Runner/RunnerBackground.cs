using UnityEngine;

public class RunnerBackground : MonoBehaviour
{
    [SerializeField] private RunnerGame game;

    [SerializeField] private Transform bkg1;
    [SerializeField] private Transform bkg2;

    [SerializeField] private float bkgSpeed;
    [SerializeField] private float bkgXDiePosition;
    [SerializeField] private Vector3 bkgRespawnPosition;

    private void Update()
    {
        if (!game.isPaused)
        {
            bkg1.transform.Translate(-Time.fixedDeltaTime * bkgSpeed * game.RushSpeedMultiplier, 0, 0);
            if (bkg1.localPosition.x < bkgXDiePosition)
                bkg1.localPosition = bkgRespawnPosition;

            bkg2.transform.Translate(-Time.fixedDeltaTime * bkgSpeed * game.RushSpeedMultiplier, 0, 0);
            if (bkg2.localPosition.x < bkgXDiePosition)
                bkg2.localPosition = bkgRespawnPosition;
        }
    }
}
