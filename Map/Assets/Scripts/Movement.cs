using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject mapper;

    private Vector3[] path;
    private int currentIndex = 0;
    private bool reachdestinaton;

    void Start()
    {
        reachdestinaton = false;
        // Get the path points from the Mapper script
        LineRenderer lineRenderer = mapper.GetComponent<LineRenderer>();
        path = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(path);

        // Set the initial position of the whale
        transform.position = path[0];

    }

    void Update()
    {

        // Move the whale along the path
        if (reachdestinaton == false)
        {
            if (currentIndex < path.Length - 1)
            {

                float distanceToNextPoint = Vector3.Distance(transform.position, path[currentIndex + 1]);

                if (distanceToNextPoint < speed * Time.deltaTime)
                {

                    transform.position = new Vector3(path[currentIndex + 1][0], 1f, path[currentIndex + 1][1]);
                    currentIndex++;

                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[currentIndex + 1][0], 1f, path[currentIndex + 1][1]), speed * Time.deltaTime);
                }

            }
            else
            {
                reachdestinaton = true;
            }

        }
        else
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                float distanceToNextPoint = Vector3.Distance(transform.position, path[currentIndex]);

                if (distanceToNextPoint < speed * Time.deltaTime)
                {
                    transform.position = new Vector3(path[currentIndex][0], 1f, path[currentIndex][1]);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[currentIndex][0], 1f, path[currentIndex][1]), speed * Time.deltaTime);
                }

            }
            else
            {
                reachdestinaton = false;
            }
        }
    }
}
