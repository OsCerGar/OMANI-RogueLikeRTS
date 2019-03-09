using UnityEngine;

public class ConLaser : MonoBehaviour
{
    public LayerMask LM;
    public float maxLength = 16.0f;
    public GameObject hitEffect;
    public Renderer meshRenderer1;
    public Renderer meshRenderer2;
    public ParticleSystem[] hitPsArray;
    public int segmentCount = 32;
    public float globalProgressSpeed = 1f;
    public AnimationCurve shaderProgressCurve;
    public AnimationCurve lineWidthCurve;
    public Light pl;
    public float moveHitToSource;
    public float WidthMultiplayer;
    private LineRenderer lr;
    private Vector3[] resultVectors;
    private float dist;
    private float globalProgress;
    private Vector3 hitPosition;
    private Vector3 currentPosition;
    CableComponentLaser CCL;
    ConLaser CLaser;

    private bool connected;
    public bool Connected { get => connected; set => connected = value; }

    void Start()
    {
        CCL = GetComponent<CableComponentLaser>();
        CLaser = GetComponentInChildren<ConLaser>();
        globalProgress = 1f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segmentCount;
        resultVectors = new Vector3[segmentCount + 1];
        for (int i = 0; i < segmentCount + 1; i++)
        {
            resultVectors[i] = transform.forward;
        }
        hitEffect.transform.SetParent(null);

    }

    void Update()
    {
        if (!connected)
        {
            //Curvy Start

            for (int i = segmentCount - 1; i > 0; i--)
            {
                resultVectors[i] = resultVectors[i - 1];
            }
            resultVectors[0] = transform.forward;
            resultVectors[segmentCount] = resultVectors[segmentCount - 1];
            float blockLength = maxLength / segmentCount;


            currentPosition = new Vector3(0, 0, 0);

            for (int i = 0; i < segmentCount; i++)
            {
                currentPosition = transform.position;
                for (int j = 0; j < i; j++)
                {
                    currentPosition += resultVectors[j] * blockLength;
                }
                lr.SetPosition(i, currentPosition);
            }

            //Curvy End



            //Collision Start

            for (int i = 0; i < segmentCount; i++)
            {

                currentPosition = transform.position;
                for (int j = 0; j < i; j++)
                {
                    currentPosition += resultVectors[j] * blockLength;
                }

                RaycastHit hit;
                if (Physics.Raycast(currentPosition, resultVectors[i], out hit, blockLength, LM))
                {
                    hitPosition = currentPosition + resultVectors[i] * hit.distance;
                    hitPosition = Vector3.MoveTowards(hitPosition, transform.position, moveHitToSource);
                    if (hitEffect)
                    {
                        hitEffect.transform.position = hitPosition;
                    }

                    dist = Vector3.Distance(hitPosition, transform.position);

                    break;
                }
            }

            //Collision End


            //Emit Particles on Collision Start

            if (hitEffect)
            {
                if (globalProgress < 0.75f)
                {
                    foreach (ParticleSystem ps in hitPsArray)
                    {
                        pl.enabled = true;

                        var em = ps.emission;
                        em.enabled = true;
                        //ps.enableEmission = true;
                    }
                }
                else
                {
                    foreach (ParticleSystem ps in hitPsArray)
                    {
                        pl.enabled = false;

                        var em = ps.emission;
                        em.enabled = false;
                        //ps.enableEmission = false;
                    }
                }
            }
        }
        else
        {
            hitEffect.transform.position = CCL.endPoint.position;
            dist = 25f;
        }
        //Emit Particles on Collision End

        GetComponent<Renderer>().material.SetFloat("_Distance", dist);
        GetComponent<Renderer>().material.SetVector("_Position", transform.position);



        if (globalProgress <= 1f)
        {
            globalProgress += Time.deltaTime * globalProgressSpeed;
        }

        if (hitEffect)
        {
            pl.intensity = shaderProgressCurve.Evaluate(globalProgress) * 1.5f;
        }

        float progress = shaderProgressCurve.Evaluate(globalProgress);
        GetComponent<Renderer>().material.SetFloat("_Progress", progress);

        if (meshRenderer1 != null && meshRenderer2 != null)
        {
            meshRenderer1.material.SetFloat("_Progress", progress);
            meshRenderer2.material.SetFloat("_Progress", progress);
        }

        float width = lineWidthCurve.Evaluate(globalProgress) * WidthMultiplayer;
        lr.widthMultiplier = width;


    }

    public void SetGlobalProgress()
    {
        globalProgress = 0f;
    }
}
