using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float fullDayLength = 100.0f;  // Length of a full day in custom units (0 to 100)
    public float speed = 1.0f;            // Speed of time progression
    private float currentTime = 0.0f;     // Current time in custom units
    public DayPhase startingPhase = DayPhase.Dawn; // Starting phase of the day cycle

    public Light sun;                     // Directional light representing the sun
    public Gradient sunColorGradient;     // Gradient for sun color at different times of day

    public enum DayPhase { Night, Dawn, Day, Dusk }
    private DayPhase currentPhase = DayPhase.Night;

    void Start()
    {
        // Set the current time based on the starting phase
        switch (startingPhase)
        {
            case DayPhase.Night:
                currentTime = 0.0f;
                break;
            case DayPhase.Dawn:
                currentTime = fullDayLength * 0.25f;
                break;
            case DayPhase.Day:
                currentTime = fullDayLength * 0.5f;
                break;
            case DayPhase.Dusk:
                currentTime = fullDayLength * 0.75f;
                break;
        }
    }

    void Update()
    {
        // Update time based on speed and time
        currentTime += speed * Time.deltaTime;

        // Normalize current time to [0, fullDayLength] range
        if (currentTime > fullDayLength)
        {
            currentTime -= fullDayLength;
        }

        // Determine current phase based on time of day
        UpdatePhase();

        // Update sun's position and color based on time of day
        UpdateSun();
    }

    void UpdatePhase()
    {
        float ratio = currentTime / fullDayLength;

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
        Color sunColor = sunColorGradient.Evaluate(currentTime / fullDayLength);

        switch (currentPhase)
        {
            case DayPhase.Night:
                angle = Mathf.Lerp(-90f, 0f, currentTime / (fullDayLength * 0.25f));
                break;
            case DayPhase.Dawn:
                angle = Mathf.Lerp(0f, 90f, (currentTime - fullDayLength * 0.25f) / (fullDayLength * 0.25f));
                break;
            case DayPhase.Day:
                angle = Mathf.Lerp(90f, 180f, (currentTime - fullDayLength * 0.5f) / (fullDayLength * 0.25f));
                break;
            case DayPhase.Dusk:
                angle = Mathf.Lerp(180f, 270f, (currentTime - fullDayLength * 0.75f) / (fullDayLength * 0.25f));
                break;
        }

        sun.transform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        sun.color = sunColor;

        // Update the ambient light color to match the sun's color
        RenderSettings.ambientLight = sunColor;
    }

    // Function to get the current time percentage
    public float GetTimePercentage()
    {
        return currentTime / fullDayLength * 100.0f;
    }
}
