﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

public void StartGame() {
    //Load the scene with a build index
    SceneManager.LoadScene(1);
}
}
