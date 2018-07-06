namespace Sitecore.Support.XConnect
{
  using Sitecore.XConnect.Operations;
  using Sitecore.XConnect;
  using System;
  using Sitecore.XConnect.Collection.Model;
  using System.Collections.Generic;
  using Sitecore.XConnect.Client;

  public static class XdbContextOperationExtensions
  {
    public static RightToBeForgottenOperation ExecuteRightToBeForgotten_Fixed(this IXdbContext context, IEntityReference<Contact> contact)
    {
      if (context == null)
      {
        throw new ArgumentNullException("context");
      }
      if (contact == null)
      {
        throw new ArgumentNullException("contact");
      }
      RightToBeForgottenOperation operation = new RightToBeForgottenOperation(contact);
      context.RegisterOperation(operation);
      #region Added code
      // The fix: remove Subcriptions from ListSubscriptions facet of anonymized contact
      RemoveContactListSubscriptions(context, contact);
      #endregion
      return operation;
    }
    #region Added code
    private static void RemoveContactListSubscriptions(IXdbContext context, IEntityReference<Contact> contactReference)
    {
      Sitecore.XConnect.Contact contact = context.Get<Sitecore.XConnect.Contact>(contactReference, new ExpandOptions(ListSubscriptions.DefaultFacetKey));
      var listSubscriptionsFacet = contact.GetFacet<ListSubscriptions>(ListSubscriptions.DefaultFacetKey);
      if (listSubscriptionsFacet != null)
      {
        listSubscriptionsFacet.Subscriptions = new List<ContactListSubscription>();
        context.SetFacet(contact, ListSubscriptions.DefaultFacetKey, listSubscriptionsFacet);
        context.Submit();
      }
    }
    #endregion 

  }
}
