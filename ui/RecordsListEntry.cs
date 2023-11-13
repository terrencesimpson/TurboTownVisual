using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class RecordsListEntry
    {
        private readonly string RECORD_ENTRY = "Prefabs/RecordEntry";
        private readonly string RECORD_TEXT = "recordText";

        private GameObject _entry;
        private Text _recordText;

        public RecordsListEntry(GameObject container, string id, double amount)
        {
            _entry = GameObjectUtility.instantiateGameObject(RECORD_ENTRY);

            _recordText = _entry.transform.Find(RECORD_TEXT).gameObject.GetComponent<Text>();

            _entry.transform.SetParent(container.transform);

            updateEntry(id, amount);
        }

        public void updateEntry(string id, double amount)
        {
            _recordText.text = id + ": " + amount;
        }


    }
}
