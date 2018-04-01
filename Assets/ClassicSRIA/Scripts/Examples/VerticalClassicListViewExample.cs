using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using frame8.ScrollRectItemsAdapter.Classic.Examples.Common;
using frame8.ScrollRectItemsAdapter.Classic.Util;
using System;
using System.Collections;

namespace frame8.ScrollRectItemsAdapter.Classic.Examples
{
    /// <summary>Class (initially) implemented during this YouTube tutorial: https://youtu.be/aoqq_j-aV8I (which is now too old to relate). It demonstrates a simple use case with items that expand/collapse on click</summary>
    public class VerticalClassicListViewExample : ClassicSRIA<SimpleExpandableClientViewsHolder>, CExpandCollapseOnClick.ISizeChangesHandler
	{
		public RectTransform itemPrefab;
		public string[] sampleFirstNames;//, sampleLastNames;
		public string[] sampleLocations;
        public Sprite[] sampleAvatar;
        public DemoUI demoUI;
		public List<ExpandableSimpleClientModel> Data { get; private set; }
        public static int countIndex;
		LayoutElement _PrefabLayoutElement;
		// Used to quickly retrieve the views holder given the gameobject
		Dictionary<RectTransform, SimpleExpandableClientViewsHolder> _MapRootToViewsHolder = new Dictionary<RectTransform, SimpleExpandableClientViewsHolder>();

        int index = 1, baseindex = 7;

        int level = 1;

        public int reqLevel2;
        public int reqLevel3;
        public int reqLevel4;
        public int reqLevel5;
        public int reqLevel6;
        public int reqLevel7;
        public int reqLevel8;
        public int reqLevel9;

        int requireExp;

        public Text level_text;
        public Text exp_text;

        #region ClassicSRIA implementation
        protected override void Awake()
		{
			base.Awake();

			Data = new List<ExpandableSimpleClientModel>();
			_PrefabLayoutElement = itemPrefab.GetComponent<LayoutElement>();
		}
       
		protected override void Start()
		{
			base.Start();

            ChangeModelsAndReset(demoUI.SetCountValue);

            requireExp = reqLevel2;
            demoUI.setCountButton.onClick.AddListener(OnItemCountChangeRequested);
			demoUI.scrollToButton.onClick.AddListener(OnScrollToRequested);
			demoUI.addOneTailButton.onClick.AddListener(() => OnAddItemRequested(true));
			demoUI.addOneHeadButton.onClick.AddListener(() => OnAddItemRequested(false));
			demoUI.removeOneTailButton.onClick.AddListener(() => OnRemoveItemRequested(true));
			demoUI.removeOneHeadButton.onClick.AddListener(() => OnRemoveItemRequested(false));

			StartCoroutine(DelayedClick());
            for(int i = index;i<= baseindex;i++)
            {
                OnAddItemRequested(true);
            }

		}

		IEnumerator DelayedClick()
		{
			yield return new WaitForSeconds(.4f);

			if (viewsHolders.Count > 0)
				viewsHolders[Mathf.Min(3, Data.Count - 1)].expandCollapseComponent.OnClicked();
		}

		protected override SimpleExpandableClientViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new SimpleExpandableClientViewsHolder();
			instance.Init(itemPrefab, itemIndex);
			instance.expandCollapseComponent.sizeChangesHandler = this;
			_MapRootToViewsHolder[instance.root] = instance;

			return instance;
		}

		protected override void UpdateViewsHolder(SimpleExpandableClientViewsHolder vh) { vh.UpdateViews(Data[vh.ItemIndex]); }
        #endregion

        #region events from DrawerCommandPanel
        void OnAddItemRequested(bool atEnd)
		{
			int index = atEnd ? Data.Count : 0;
            countIndex = Data.Count;
           // Debug.Log("Index "+Data.Count);
			Data.Insert(index, CreateNewModel(index));
			InsertItems(index, 1, demoUI.freezeContentEndEdge.isOn);
		}

		void OnRemoveItemRequested(bool fromEnd)
		{
			if (Data.Count == 0)
				return;

			int index = fromEnd ? Data.Count - 1 : 0;

			Data.RemoveAt(index);
			RemoveItems(index, 1, demoUI.freezeContentEndEdge.isOn);
		}
		void OnItemCountChangeRequested() { ChangeModelsAndReset(demoUI.SetCountValue); }
		void OnScrollToRequested()
		{
			if (demoUI.ScrollToValue >= Data.Count)
				return;

			demoUI.scrollToButton.interactable = false;
			bool started = SmoothScrollTo(demoUI.ScrollToValue, .75f, .5f, .5f, () => demoUI.scrollToButton.interactable = true);
			if (!started)
				demoUI.scrollToButton.interactable = true;
		}
		#endregion

		#region CExpandCollapseOnClick.ISizeChangesHandler implementation
		public bool HandleSizeChangeRequest(RectTransform rt, float newSize)
		{
			_MapRootToViewsHolder[rt].layoutElement.preferredHeight = newSize;
			return true;
		}

		public void OnExpandedStateChanged(RectTransform rt, bool expanded)
		{
			var itemIndex = _MapRootToViewsHolder[rt].ItemIndex;
			Data[itemIndex].expanded = expanded;
		}
		#endregion

		void ChangeModelsAndReset(int newCount)
		{
			Data.Clear();
			Data.Capacity = newCount;
			for (int i = 0; i < newCount; i++)
			{
				var model = CreateNewModel(i);
				Data.Add(model);
			}

			ResetItems(Data.Count);
		}

		ExpandableSimpleClientModel CreateNewModel(int index)
		{
            level_text.text = "Level " + level;
            exp_text.text = "Next Level" + demoUI.exp + " / " + requireExp;
            var model = new ExpandableSimpleClientModel()
			{
				clientName = sampleFirstNames[CUtil.Rand(sampleFirstNames.Length)],
				location = sampleLocations[CUtil.Rand(sampleLocations.Length)],
                avatarPic = sampleAvatar[Data.Count],
				nonExpandedSize = _PrefabLayoutElement.preferredHeight
			};
			model.SetRandom();
            demoUI.exp += 1;
            Debug.Log(demoUI.exp);
            ChangeLevel();

            return model;
		}

        void ChangeLevel()
        {
            if (demoUI.exp >= reqLevel2 && level == 1)
            {
                requireExp = reqLevel3;
                level = 2;
                Debug.Log("level up" + level);
            }
            else if (demoUI.exp >= reqLevel3 && level == 2)
            {
                requireExp = reqLevel4;
                level = 3;
                Debug.Log("level up" + level);
            }
            else if (demoUI.exp >= reqLevel4 && level == 3)
            {
                requireExp = reqLevel5;
                level = 4;
                Debug.Log("level up" + level);
            }
            else if (demoUI.exp >= reqLevel5 && level == 4)
            {
                //requireExp = reqLevel6;
                level = 5;
                Debug.Log("level up" + level);
            }
            level_text.text = "Level " + level;
            exp_text.text = "Next Level : " + demoUI.exp + " / " + requireExp;

        }
    }	
}
