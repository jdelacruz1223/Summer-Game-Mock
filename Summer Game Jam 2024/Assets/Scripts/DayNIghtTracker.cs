using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float fullDayLengthInSeconds = 120f; // Length of a full day in seconds
    public Light sun; // Directional light representing the sun
    public Gradient sunColorGradient; // Gradient for sun color at different times of day

    private enum DayPhase { Night, Dawn, Day, Dusk }
    private DayPhase currentPhase = DayPhase.Night;
    private float currentTimeOfDay = 0f; // Current time of day in seconds

    void Update()
    {
        // Update the time of day
        currentTimeOfDay += Time.deltaTime;

        // Normalize current time of day to [0, fullDayLengthInSeconds] range
        if (currentTimeOfDay > fullDayLengthInSeconds)
        {
            currentTimeOfDay -= fullDayLengthInSeconds;
        }

        // Determine current phase based on time of day
        UpdatePhase();

        // Update sun's position and color based on time of day
        UpdateSun();
    }

    void UpdatePhase()
    {
        float ratio = currentTimeOfDay / fullDayLengthInSeconds;

        if (ratio < 0.25f)
        {
            currentPhase = DayPhase.Night;
        }
        else if (ratio < 0.5f)
        {
            currentPhase = DayPhase.Dawn;
        }
        else if (ratio < 0.75f)
        {
            currentPhase = DayPhase.Day;
        }
        else
        {
            currentPhase = DayPhase.Dusk;
        }
    }

    void UpdateSun()
    {
        float angle = 0f;
        Color sunColor = Color.white;

        switch (currentPhase)
        {
            case DayPhase.Night:
                angle = Mathf.Lerp(-90f, 0f, currentTimeOfDay / (fullDayLengthInSeconds * 0.25f));
                sunColor = Color.black;
                break;
            case DayPhase.Dawn:
                angle = Mathf.Lerp(0f, 90f, (currentTimeOfDay - fullDayLengthInSeconds * 0.25f) / (fullDayLengthInSeconds * 0.25f));
                sunColor = sunColorGradient.Evaluate((currentTimeOfDay - fullDayLengthInSeconds * 0.25f) / (fullDayLengthInSeconds * 0.25f));
                break;
            case DayPhase.Day:
                angle = Mathf.Lerp(90f, 180f, (currentTimeOfDay - fullDayLengthInSeconds * 0.5f) / (fullDayLengthInSeconds * 0.25f));
                sunColor = sunColorGradient.Evaluate((currentTimeOfDay - fullDayLengthInSeconds * 0.5f) / (fullDayLengthInSeconds * 0.25f));
                break;
            case DayPhase.Dusk:
                angle = Mathf.Lerp(180f, 270f, (currentTimeOfDay - fullDayLengthInSeconds * 0.75f) / (fullDayLengthInSeconds * 0.25f));
                sunColor = sunColorGradient.Evaluate((currentTimeOfDay - fullDayLengthInSeconds * 0.75f) / (fullDayLengthInSeconds * 0.25f));
                break;
        }

        sun.transform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        sun.color = sunColor;
    }
}
