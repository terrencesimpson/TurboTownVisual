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
    public class BuildingView
    {
        public static readonly float FOOTPRINT_HEIGHT = 1;
        public static readonly float FOOTPRINT_Y_OFFSET = 10;
        public static readonly float ARROW_OFFSET = 5;
        public static readonly float ARROW_SCALE = 5;
        public static readonly float DEFAULT_HEIGHT = 1;
        public static readonly float INTERACTIVE_HEIGHT = 10;
        public static readonly float INTERACTIVE_GRID_MULTIPLIER_FOR_MOBILE = 8;
        public static readonly float INTERACTIVE_GRID_MULTIPLIER_FOR_DESKTOP = 8;

        public static readonly string FOOTPRINT_PATH = "Prefabs/building/Footprint";
        public static readonly string FOOTPRINT_ARROW_PATH = "Prefabs/building/Footprint_Arrow";
        public static readonly string ARROW = "arrow";

        public List<Material[]> originalMaterials;

        private GameObject _view;
        public GameObject view { get { return _view; } }

        public Vector3 size { get { return _colliderSize; } }

        public string facingDirection = DirectionEnum.SOUTH;

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

        public Vector3 objectViewSize; //You HAVE to track this. This is for the rotation of the building, you can't use the Box Collider as we scale that for interactivity. This is used with the GridNode.
        public Vector3 _colliderSize; //Even though the bounds.size of a Collider shows as something like 20, 1, 10 it comes back with the X and Z swapped when a building is rotated. Makes no sense. I will track it myself so the collider is measured correctly.

        public GameObject footprint;
        public GameObject arrowsContainer;

        private string _prefabPath = "";
        public string prefabPath { get { return _prefabPath; } }

        public InteractiveComponent interactiveComponent { get { return _view.GetComponent<InteractiveComponent>(); } }

        public BuildingView(string prefabPath)
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

            Vector3 oCS = _view.GetComponent<BoxCollider>().bounds.size; //Original Collider Size
            objectViewSize = new Vector3(oCS.x, oCS.y, oCS.z); //The Box Collider is the source of truth at this point.
            _colliderSize = new Vector3(oCS.x, oCS.y, oCS.z); //Used for the Collider scaling for interactivity. The objectViewSize gets rotated, this doesn't and bounds.size fucks up for some reason. 

            _prefabPath = newPrefabPath;
        }

        public void createFootprintMoverArrows()
        {
            createFootprint();
            initArrows();
            hideFootprint();
        }

        private void initArrows()
        {
            arrowsContainer = new GameObject();
            arrowsContainer.name = "ArrowContainer";
            arrowsContainer.transform.SetParent(_view.transform);
            arrowsContainer.transform.localPosition = new Vector3(0, -FOOTPRINT_Y_OFFSET, 0);
            footprint.transform.localRotation = rotation;

            GameObject northArrowContainer = GameObjectUtility.instantiateGameObject(FOOTPRINT_ARROW_PATH);
            GameObject eastArrowContainer = GameObjectUtility.instantiateGameObject(FOOTPRINT_ARROW_PATH);
            GameObject southArrowContainer = GameObjectUtility.instantiateGameObject(FOOTPRINT_ARROW_PATH);
            GameObject westArrowContainer = GameObjectUtility.instantiateGameObject(FOOTPRINT_ARROW_PATH);

            northArrowContainer.name = "NorthArrowContainer";
            eastArrowContainer.name = "EastArrowContainer";
            southArrowContainer.name = "SouthArrowContainer";
            westArrowContainer.name = "WestArrowContainer";

            GameObject northArrow = northArrowContainer.transform.Find(ARROW).gameObject;
            GameObject eastArrow = eastArrowContainer.transform.Find(ARROW).gameObject;
            GameObject southArrow = southArrowContainer.transform.Find(ARROW).gameObject;
            GameObject westArrow = westArrowContainer.transform.Find(ARROW).gameObject;

            northArrow.name = "NorthArrow";
            eastArrow.name = "EastArrow";
            southArrow.name = "SouthArrow";
            westArrow.name = "WestArrow";

            northArrowContainer.transform.SetParent(arrowsContainer.transform);
            eastArrowContainer.transform.SetParent(arrowsContainer.transform);
            southArrowContainer.transform.SetParent(arrowsContainer.transform);
            westArrowContainer.transform.SetParent(arrowsContainer.transform);

            arrowsContainer.transform.localScale = new Vector3(arrowsContainer.transform.localScale.x, ARROW_SCALE, arrowsContainer.transform.localScale.z);

            GameObjectUtility.rotateGameObject(northArrow, 90, 0, 270);
            GameObjectUtility.rotateGameObject(eastArrow, 90, 0, 180);
            GameObjectUtility.rotateGameObject(southArrow, 90, 0, 90);

            northArrowContainer.transform.localPosition = new Vector3(-(_colliderSize.x / 2) - ARROW_OFFSET, 0, 0);
            eastArrowContainer.transform.localPosition = new Vector3(0, 0, (_colliderSize.z / 2) + ARROW_OFFSET);
            southArrowContainer.transform.localPosition = new Vector3((_colliderSize.x / 2) + ARROW_OFFSET, 0, 0);
            westArrowContainer.transform.localPosition = new Vector3(0, 0, -(_colliderSize.z / 2) - ARROW_OFFSET);
        }

        public void positionToGround()
        {
            if (position.y > 0)
            {
                iTween.MoveBy(view, iTween.Hash("y", -FOOTPRINT_Y_OFFSET, "islocal", true, "time", .2f, "easetype", "easeOutCubic"));
            }
        }

        public void positionToSky()
        {
            iTween.MoveBy(view, iTween.Hash("y", FOOTPRINT_Y_OFFSET, "islocal", true, "time", .2f, "easetype", "easeOutCubic"));
        }

        public void rotateObjectView(float x, float y, float z, bool animate)
        {
            if (animate) iTween.RotateBy(view, iTween.Hash("y", .25, "easeType", "easeOutElastic", "time", .8f));
            else rotation = (Quaternion.Euler(new Vector3(x, y, z)));
        }

        public bool isTweening()
        {
            if (iTween.Count(view) > 0) return true;
            return false;
        }

        public void rotateObjectViewSize()
        {
            objectViewSize.Set(objectViewSize.z, objectViewSize.y, objectViewSize.x);
        }

        public void setColliderToInteractive()
        {
            float interactiveSize = getInteractiveSize();
            Vector3 newSize = new Vector3(size.x + interactiveSize, INTERACTIVE_HEIGHT, size.z + interactiveSize); //Add to the original collider values
            modifyColliderSize(newSize.x, newSize.y, newSize.z);
        }

        public void setColliderToDefault()
        {
            float interactiveSize = getInteractiveSize();
            Vector3 newSize = new Vector3(size.x, DEFAULT_HEIGHT, size.z); //The values have not been modified, so just set them back to the collider
            modifyColliderSize(newSize.x, newSize.y, newSize.z);
        }

        public void modifyColliderSize(float x, float y, float z)
        {
            BoxCollider boxCollider = _view.GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(x, y, z);
        }

        private float getInteractiveSize()
        {
            float interactiveSize = GridManager.SIZE_FACTOR * INTERACTIVE_GRID_MULTIPLIER_FOR_MOBILE;
            if (!PlatformUtility.isMobile()) interactiveSize = GridManager.SIZE_FACTOR * INTERACTIVE_GRID_MULTIPLIER_FOR_DESKTOP;
            return interactiveSize;
        }

        public void showFootprint()
        {
            createFootprint();
            footprint.SetActive(true);
            arrowsContainer.SetActive(true);
        }

        private void createFootprint()
        {
            if (footprint == null)
            {
                footprint = GameObjectUtility.instantiateGameObject(FOOTPRINT_PATH);
                footprint.transform.SetParent(_view.transform);
                footprint.transform.localPosition = new Vector3(0, -FOOTPRINT_Y_OFFSET, 0);
                footprint.transform.localScale = new Vector3(size.x, FOOTPRINT_HEIGHT, size.z); //Models the initial box collider
                footprint.transform.localRotation = rotation;
            }
        }

        public void hideFootprint()
        {
            if (footprint != null) footprint.SetActive(false);
            if (arrowsContainer != null) arrowsContainer.SetActive(false);
        }

        public bool hasCubieContainers()
        {
            Transform trans = _view.transform.Find(CubiePropertiesEnum.CUBIE_CONTAINER);
            if (trans != null) return true;
            return false;
        }

        public List<GameObject> getCubieContainers()
        {
            GameObject cubieContainer = _view.transform.Find(CubiePropertiesEnum.CUBIE_CONTAINER).transform.gameObject;
            int childCount = cubieContainer.transform.childCount;
            List<GameObject> children = new List<GameObject>();
            for (int i = 0; i < childCount; ++i)
            {
                children.Add(cubieContainer.transform.GetChild(i).gameObject);
            }
            return children;
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
                GameObject.Destroy(footprint);
            }
            originalMaterials = null;
        }

    }
}
