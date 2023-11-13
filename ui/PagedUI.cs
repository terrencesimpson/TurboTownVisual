using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.super2games.idle.utilties;

namespace com.super2games.idle.ui
{
    public class PagedUI : MonoBehaviour
    {
        private readonly string PREV_BUTTON = "prevButton";
        private readonly string NEXT_BUTTON = "nextButton";
        private readonly string PAGE_TEXT = "pageText";

        private readonly float KEY_PRESS_BUFFER = .2f;

        private string _contentPrefabURL;
        private int _entriesPerPage;
        private PrefabManager _prefabManager;
        private GameObject _pagesContainer;
        private GameObject _pageBtnsContainer;
        private Button _prevButton;
        private Button _nextButton;
        private Text _pageText;
        private List<GameObject> _pages = new List<GameObject>();
        private List<GameObject> _entries = new List<GameObject>();
        private int _currentPageIndex = 0;
        private float _keyPressBuffer = 0f;

        private bool _hasStarted = false;

        public void init(string contentPrefabURL, GameObject pagesContainer, GameObject pageBtnsContainer, PrefabManager prefabManager, int entriesPerPage)
        {
            _contentPrefabURL = contentPrefabURL;
            _pagesContainer = pagesContainer;
            _pageBtnsContainer = pageBtnsContainer;
            _prefabManager = prefabManager;
            _entriesPerPage = entriesPerPage;

            Transform t = _pageBtnsContainer.transform.Find(NEXT_BUTTON);

            _nextButton = _pageBtnsContainer.transform.Find(NEXT_BUTTON).gameObject.GetComponent<Button>();
            _prevButton = _pageBtnsContainer.transform.Find(PREV_BUTTON).gameObject.GetComponent<Button>();
            _pageText = _pageBtnsContainer.transform.Find(PAGE_TEXT).gameObject.GetComponent<Text>();

            _prevButton.onClick.AddListener(onBackClick);
            _nextButton.onClick.AddListener(onNextClick);

            createNewPage();

            _hasStarted = true;
        }

        void Update()
        {
            if (!_hasStarted)
            {
                return;
            }

            _keyPressBuffer += Time.deltaTime;

            if (_keyPressBuffer < KEY_PRESS_BUFFER)
            {
                return;
            }

            if (InputManager.key_down_Z && _pagesContainer.activeInHierarchy)
            {
                modifyPageIndex(-1);
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_X && _pagesContainer.activeInHierarchy)
            {
                modifyPageIndex(1);
                _keyPressBuffer = 0;
            }
        }

        public void showPage(int index = 0)
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].SetActive(false);
            }

            _pages[index].SetActive(true);
            _currentPageIndex = index;
            updatePageText();
        }

        public void addEntryToPage(GameObject entry, bool addToEntriesList = true)
        {
            GameObject page = _pages[_pages.Count - 1];

            if (page.transform.childCount >= _entriesPerPage)
            {
                page = createNewPage();
                page.SetActive(false);
                updatePageText();
            }

            entry.transform.SetParent(page.transform);
            entry.transform.localScale = new Vector3(1, 1, 1);

            if (addToEntriesList)
            {
                _entries.Add(entry);
            }
        }

        //Must rebuild list to remove an entry since things need to be shifted between parents
        public void removeEntry(GameObject entry)
        {
            entry.transform.SetParent(null);

            foreach (GameObject e in _entries)
            {
                if (entry == e)
                {
                    _entries.Remove(entry);
                    break;
                }
            }

            rebuildPages();

            //in case the very last entry is removed, move back a page
            if (_currentPageIndex > _pages.Count-1)
            {
                _currentPageIndex = _pages.Count - 1;
            }

            showPage(_currentPageIndex);
        }

        private void rebuildPages()
        {
            clearAllPages();

            for (int i = 0; i < _entries.Count; i++)
            {
                addEntryToPage(_entries[i], false);
            }
        }

        //untested!
        public void clearAllPages(bool clearEntriesToo = false)
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].transform.DetachChildren();
                GameObject.Destroy(_pages[i]);
            }
            _pages.Clear();

            if (clearEntriesToo)
            {
                _entries.Clear();
            }
            createNewPage();
        }

        private GameObject createNewPage()
        {
            GameObject page = _prefabManager.getPrefab(_contentPrefabURL);

            page.transform.SetParent(_pagesContainer.transform, false);
            _pages.Add(page);

            return page;
        }

        private void onBackClick()
        {
            modifyPageIndex(-1);
        }

        private void onNextClick()
        {
            modifyPageIndex(1);
        }

        private void modifyPageIndex(int modifier)
        {
            _currentPageIndex += modifier;

            if (_currentPageIndex < 0)
            {
                _currentPageIndex = (_pages.Count - 1);
            }
            else if (_currentPageIndex > _pages.Count - 1)
            {
                _currentPageIndex = 0;
            }

            showPage(_currentPageIndex);
        }

        private void updatePageText()
        {
            string currentPageText = (_currentPageIndex + 1).ToString();
            string lastPageText = _pages.Count.ToString();
            _pageText.text = _currentPageIndex + 1 + " / " + _pages.Count;
        }

        public void onPrevButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Previous Page" : "Previous Page (Z)";
            TooltipUI.instance.displaySimpleTooltip(text, _prevButton.gameObject.transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC);
        }

        public void onNextButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Next Page" : "Next Page (X)";
            TooltipUI.instance.displaySimpleTooltip(text, _nextButton.gameObject.transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC);
        }

        public void onButtonMouseOut()
        {
            TooltipUI.instance.hideTooltip();
        }

        public int numberOfPages()
        {
            return _pages.Count;
        }
    }
}
