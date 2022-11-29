public class DynamicCrosshair : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] RectTransform mousePanel;
        [SerializeField] float minSize;
        [SerializeField] float maxSize;

        Camera uiCamera;
        float currentSize;
        bool playerShooting = false;

        private void Start()
        {
            GameManager.S.GameEventHandler.onPlayerShootStart += StartIncreasing;
            GameManager.S.GameEventHandler.onPlayerShootEnd += StopIncreasing;

            uiCamera = GetComponent<Camera>();
        }

        void StartIncreasing()
        {
            playerShooting = true;
        }

        void StopIncreasing()
        {
            playerShooting = false;
        }

        private void Update()
        {
            FollowMousePos();

            if (playerShooting)
            {
                currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * 10f);

                mousePanel.sizeDelta = new Vector2(currentSize, currentSize);
            }
            else
            {
                currentSize = Mathf.Lerp(currentSize, minSize, Time.deltaTime * 5f);

                mousePanel.sizeDelta = new Vector2(currentSize, currentSize);
            }
        }

        void FollowMousePos()
        {
            Vector3 mousePos = uiCamera.ScreenToWorldPoint(Input.mousePosition);

            mousePos.Set(mousePos.x, mousePos.y, 0f);

            mousePanel.position = mousePos;
        }

        private void OnDestroy()
        {
            GameManager.S.GameEventHandler.onPlayerShootStart -= StartIncreasing;
            GameManager.S.GameEventHandler.onPlayerShootEnd -= StopIncreasing;
        }
    }
