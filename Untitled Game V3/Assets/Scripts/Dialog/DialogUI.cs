using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text textLable;
    [SerializeField] private DialogObject textDialog;
    private TypewriterEffect twe;
    // Start is called before the first frame update
    void Start()
    {
        twe = GetComponent<TypewriterEffect>();
        CloseDialogBox();
        ShowDialog(textDialog);
    }
    public void ShowDialog(DialogObject dialogobject)
    {
        dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogobject));
    }
    private IEnumerator StepThroughDialog(DialogObject dialogobject)
    {
        foreach (string dialog in dialogobject.Dialog)
        {
            yield return twe.Run(dialog, textLable);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogBox();
    }
    private void CloseDialogBox()
    {
        dialogBox.SetActive(false);
        textLable.text = string.Empty;
    }
}
