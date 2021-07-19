/**
 * Attach to ShopOwner to trigger shop menu
 */
public class ShopInteractable : Interactable
{
    public override void Interact()
    {
        ShopMenu.scriptInstance.OpenShop();
    }
}
