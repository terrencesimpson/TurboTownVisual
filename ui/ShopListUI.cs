using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class ShopListUI
    {
        private readonly string MUSIC_BANDCAMP_URL = "https://ryaniyengar.bandcamp.com/album/trip-reporter";
        private readonly string MUSIC_SPOTIFY_URL = "https://play.spotify.com/album/0NHyq8lUVStWEKntASzTbk?play=true&utm_source=open.spotify.com&utm_medium=open";

        private readonly string CLOSE_BUTTON = "closeButtonHotspot";
        private readonly string SOLD_OUT_IMAGE = "soldOutImage";

        private readonly string RESTOCK_TEXT = "restockText";

        private readonly string MUSIC_ALBUM_IMAGE = "MusicPanel/albumImage";
        private readonly string MUSIC_BANDCAMP_TEXT = "MusicPanel/bandcampText";
        private readonly string MUSIC_SPOTIFY_TEXT = "MusicPanel/spotifyText";

        private readonly string SHOP_ENTRY = "Prefabs/ui/ShopEntry";
        private readonly string SHOP_BUCKS_ENTRY = "Prefabs/ui/ShopBucksEntry";

        private GameObject _shopPanel;

        private Button _closeButton;
        private Button _musicBandcampButton;
        private Button _musicAlbumButton;
        private Button _musicSpotifyButton;

        private UIManager _uiManager;
        private ItemsManager _itemsManager;
        private ModelManager _modelManager;
        private PrefabManager _prefabManager;

        private Text _restockText;

        private Dictionary<GameObject, ShopListEntry> _entries = new Dictionary<GameObject, ShopListEntry>();

        private bool _renderedBucksAndPeriodic = false;

        private ShopListEntry _lastClickedEntry;

        public ShopListUI(GameObject shopPanel, UIManager uiManager, ModelManager modelManager, ItemsManager itemsManager, PrefabManager prefabManager)
        {
            _shopPanel = shopPanel;
            _uiManager = uiManager;
            _modelManager = modelManager;
            _itemsManager = itemsManager;
            _prefabManager = prefabManager;

            _closeButton = _shopPanel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _musicAlbumButton = _shopPanel.transform.Find(MUSIC_ALBUM_IMAGE).gameObject.GetComponent<Button>();
            _musicBandcampButton = _shopPanel.transform.Find(MUSIC_BANDCAMP_TEXT).gameObject.GetComponent<Button>();
            _musicSpotifyButton = _shopPanel.transform.Find(MUSIC_SPOTIFY_TEXT).gameObject.GetComponent<Button>();
            _restockText = _shopPanel.transform.Find(RESTOCK_TEXT).gameObject.GetComponent<Text>();

            _closeButton.onClick.AddListener(onCloseClick);
            _musicAlbumButton.onClick.AddListener(onMusicBandcampClick);
            _musicBandcampButton.onClick.AddListener(onMusicBandcampClick);
            _musicSpotifyButton.onClick.AddListener(onMusicSpotifyClick);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideShopPanel();
        }

        private void onMusicBandcampClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            URLUtility.openURL(MUSIC_BANDCAMP_URL);
        }

        private void onMusicSpotifyClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            URLUtility.openURL(MUSIC_SPOTIFY_URL);
        }

        public void updateStoreRestockTime(double storeRestockTime)
        {
            if (_restockText != null)
            {
                _restockText.text = "Refreshes In: " + StringUtility.formatSeconds(storeRestockTime);
            }
        }

        public void populateUI()
        {
            if (!_renderedBucksAndPeriodic)
            {
                populateShopEntriesList(_itemsManager.bucksItemConfigs, _uiManager.turboBucksShopEntries, ItemsManager.TURBO_BUCKS_NUM_ENTRIES);
                populateShopEntriesList(_itemsManager.periodicItemConfigs, _uiManager.periodicShopEntries, ItemsManager.PERIODIC_ITEMS_NUM_ENTRIES);
                _renderedBucksAndPeriodic = true;
            }
            populateShopEntriesList(_itemsManager.timeWarpsItemConfigs, _uiManager.timeWarpsShopEntries, ItemsManager.STORE_ITEMS_NUM_ENTRIES);
            populateShopEntriesList(_itemsManager.boostsItemConfigs, _uiManager.boostsShopEntries, ItemsManager.STORE_ITEMS_NUM_ENTRIES);
        }

        public void displaySoldOutOverlayOnShopConfig(ShopConfig shopConfig)
        {
            foreach (KeyValuePair<GameObject, ShopListEntry> pair in _entries)
            {
                if (pair.Value.shopConfig == shopConfig)
                {
                    displaySoldOutOverlay(pair.Value);
                    return; //Found it, just one to find, get out.
                }
            }
        }

        public void displaySoldOutOverlay(ShopListEntry entry)
        {
            if (entry.entryGO.transform.Find(SOLD_OUT_IMAGE))
            {
                entry.entryGO.transform.Find(SOLD_OUT_IMAGE).gameObject.SetActive(true);
                entry.entryGO.transform.Find(SOLD_OUT_IMAGE).SetAsLastSibling();
                entry.entryGO.GetComponent<Button>().enabled = false;
            }
        }

        public void hideSoldOutOverlay(ShopListEntry entry)
        {
            if (entry.entryGO.transform.Find(SOLD_OUT_IMAGE))
            {
                entry.entryGO.transform.Find(SOLD_OUT_IMAGE).gameObject.SetActive(false);
                entry.entryGO.GetComponent<Button>().enabled = true;
            }
        }

        private void populateShopEntriesList(List<ItemConfig> itemConfigs, List<GameObject> parentList, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject entryGO = parentList[i];
                ShopListEntry entry = null;
                ItemConfig itemConfig = itemConfigs[i];
                BoostConfig boostConfig = null;
                ShopConfig  shopConfig = _itemsManager.getShopConfigByItemID(itemConfig.id);

                if (itemConfig.type == ItemTypeEnum.BOOST)
                {
                    boostConfig = _modelManager.boostsModel.getConfig(itemConfig.itemID) as BoostConfig;
                }
                else if (itemConfig.type == ItemTypeEnum.PERIODIC)
                {
                    itemConfig = _modelManager.itemsModel.getConfig(itemConfig.itemID) as ItemConfig; //Looks weird but the Periodic item refers to a Bucks item entry. This has the amount the Periodic gives.
                }

                if (!_entries.ContainsKey(entryGO))
                {
                    entry = new ShopListEntry();
                    entry.onShopEntryClickEvent += onShopEntryClick;
                    _entries.Add(entryGO, entry);
                }
                else
                {
                    entry = _entries[entryGO];
                }

                entry.updateEntry(entryGO, shopConfig, itemConfig, boostConfig);
                hideSoldOutOverlay(entry);
            }
        }

        private void onShopEntryClick(ShopListEntry entry)
        {
            _lastClickedEntry = entry;
            _uiManager.showPurchaseConfirmationPanel();
            _uiManager.purchaseConfirmationPanelUI.populateUI(entry.shopConfig, entry.itemConfig, entry.boostConfig);
            storeItemClickedAnalytics(entry.shopConfig);
        }

        public void greyOutLastEntry()
        {
            if (_lastClickedEntry == null) return;
            string entryType = _lastClickedEntry.itemConfig.type;
            string entryOwner = _lastClickedEntry.itemConfig.owner;
            if ((entryType != ItemTypeEnum.BUCKS || 
                (entryType == ItemTypeEnum.BUCKS &&
                 entryOwner == ItemOwnerTypeEnum.PERIODIC)))
            {
                _lastClickedEntry.hasSold = true;
                displaySoldOutOverlay(_lastClickedEntry);
            }
        }

        private void storeItemClickedAnalytics(ShopConfig shopConfig)
        {
            string currency = CurrencyUtility.getCurrency(shopConfig);
            if (currency == CurrencyEnum.BUCKS) JobFactory.playFabManager.analytics(AnalyticsEnum.BUCKS_STORE_ITEM_CLICKED);
            else if (currency == CurrencyEnum.KREDS) JobFactory.playFabManager.analytics(AnalyticsEnum.KREDS_STORE_ITEM_CLICKED);
            else if (currency == CurrencyEnum.DOLLAR) JobFactory.playFabManager.analytics(AnalyticsEnum.DOLLAR_STORE_ITEM_CLICKED);
        }

    }
}
