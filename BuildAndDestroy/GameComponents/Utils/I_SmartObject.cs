namespace BuildAndDestroy.GameComponents.Utils
{
     /// <summary>
     /// Cette inteface est a implémenter sur tous les objects un temp soit peu complexe afin que l'on puisse les supprimer de proprement
     /// </summary>
    public interface I_SmartObject
    {
        public void Destroy();
    }
}