using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
		 private CanvasGroup group;

		 public void Start()
		 {
					group = this.GetComponent<CanvasGroup>();
		 }

		 public void FadeOut(float time)
		 {
					StartCoroutine(_FadeOut(time));
		 }

		 public void FadeIn(float time)
		 {
					StartCoroutine(_FadeIn(time));
		 }

		 IEnumerator _FadeOut(float time)
		 {
					group.alpha = 0;
					while (group.alpha != 1)
					{
							 group.alpha += Time.deltaTime / time;
							 yield return null;
					}
		 }

		 IEnumerator _FadeIn(float time)
		 {
					group.alpha = 1;
					while (group.alpha != 0)
					{
							 group.alpha -= Time.deltaTime / time;
							 yield return null;
					}
		 }
}
