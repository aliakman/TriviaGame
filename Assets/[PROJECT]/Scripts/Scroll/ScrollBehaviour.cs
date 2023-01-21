using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : MonoBehaviour
{
    private LeaderboardGeneralData _leaderboardGeneralData;
    private LeaderboardGeneralData leaderboardGeneralData { get { return _leaderboardGeneralData ? _leaderboardGeneralData : _leaderboardGeneralData = EventManager.LeaderboardGeneralData?.Invoke(); } }

    public RectTransform leaderboardSlotRectTransform;

    private List<RectTransform> contentRectTransforms = new List<RectTransform>();

    public GameObject scrollObject;
    public Transform scrollParent;
    public float speed;
    private int offset;
    public float upValue;
    public float downValue;

    private Vector2 firstInput, lastInput, inputDifference;

    private void OnEnable()
    {
        EventManager.ScrollBehaviour += () => this;
    }
    private void OnDisable()
    {
        EventManager.ScrollBehaviour -= () => this;
    }

    public void SetScroll()
    {
        scrollObject.SetActive(true);
    }

    private void Start()
    {
        leaderboardGeneralData.SetLeaderboardData();

        scrollParent.localPosition = new Vector2(0, 0);

        for (int i = 0; i < leaderboardGeneralData.AllPersonalData.Count; i++)
        {
            contentRectTransforms.Add(Instantiate(leaderboardSlotRectTransform, scrollParent));
        }


        SetRectPosition();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            firstInput = Input.mousePosition;

        lastInput = Input.mousePosition;

        inputDifference = (firstInput - lastInput) * speed * Time.deltaTime;

        scrollParent.localPosition = new Vector2(scrollParent.localPosition.x, scrollParent.localPosition.y + inputDifference.y);

        if (inputDifference.y > 0 && offset > 0)
        {
            CheckUp();
        }
        else if (inputDifference.y <
            0)
        {
            CheckDown();
        }

        firstInput = Input.mousePosition;

    }

    private void SetRectPosition()
    {
        scrollParent.localPosition = Vector2.zero;

        for (int i = 0; i < contentRectTransforms.Count; i++)
        {
            contentRectTransforms[i].localPosition = new Vector2(0, -100 - i * contentRectTransforms[i].sizeDelta.y + 25);

            if (offset <= 0)
                offset = 1;
            contentRectTransforms[i].GetComponent<LeaderboardSlot>().Init((i + offset).ToString(), leaderboardGeneralData.AllPersonalData[(i + offset) % leaderboardGeneralData.AllPersonalData.Count].nickname, (99999 - i - offset).ToString());
        }

    }

    private void CheckUp()
    {
        if(contentRectTransforms[0].position.y > upValue) 
        {
            contentRectTransforms.Insert(contentRectTransforms.Count - 1, contentRectTransforms[0]);
            contentRectTransforms.RemoveAt(0);
            offset--;
            SetRectPosition();
        }
    }

    private void CheckDown()
    {
        if (contentRectTransforms[contentRectTransforms.Count - 1].position.y < downValue)
        {
            contentRectTransforms.Insert(0, contentRectTransforms[contentRectTransforms.Count - 1]);
            contentRectTransforms.RemoveAt(contentRectTransforms.Count - 1);
            SetRectPosition();
            offset++;
        }
    }

}
