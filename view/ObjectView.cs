using com.super2games.idle.component.interaction;
using com.super2games.idle.component.view;
using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.view
{
    public class ObjectView
    {
        private GameObject _view;
        public GameObject view { get { return _view; } }

        public Vector3 position
        {
            get { return _view.transform.position; }
            set { _view.transform.position = value; }
        }

        public Quaternion rotation
        {
            get { return _view.transform.rotation; }
            set { _view.transform.rotation = value; }
        }

        public Vector3 scale
        {
            get { return _view.transform.localScale; }
            set { _view.transform.localScale = value; }
        }

        private string _prefabPath = "";
        public string prefabPath { get { return _prefabPath; } }

        public InteractiveComponent interactiveComponent { get { return _view.GetComponent<InteractiveComponent>(); } }

        public ObjectView(string prefabPath)
        {
            changeView(prefabPath);
        }

        public void changeView(string newPrefabPath)
        {
            if (_prefabPath == newPrefabPath)
            {
                return;
            }

            if (_view != null)
            {   //View does exist, get the position, rotation and scale, destroy old view, create the new one and add the old position, rotation and scale.
                Vector3 pos = position;
                Quaternion rot = rotation;
                Vector3 localScale = scale;

                destroy();

                _view = GameObjectUtility.instantiateGameObject(newPrefabPath);

                //Setting these now that we have the game object.
                position = pos;
                rotation = rot;
                scale = localScale;
            }
            else
            {   //View does not exist, just create it
                _view = GameObjectUtility.instantiateGameObject(newPrefabPath);
            }

            _prefabPath = newPrefabPath;
        }

        public bool isTweening()
        {
            if (iTween.Count(view) > 0) return true;
            return false;
        }

        public void destroy()
        {
            if (_view != null)
            {
                BoxCollider boxCollider = _view.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {   //Ran into a case where the physics system must be holding on a reference of the GameObject and checking collisions again.
                    //This happened when you dropped a cubie onto multiple buildings.
                    boxCollider.enabled = false;
                }
                GameObject.Destroy(_view);
            }
        }

    }
}
