namespace Sitecore.XConnect
{
  using Sitecore.XConnect.Operations;
  using System;

  public static class XdbContextOperationExtensions
  {
    public static RightToBeForgottenOperation ExecuteRightToBeForgotten(this IXdbContext context, IEntityReference<Contact> contact)
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
      return operation;
    }

  }
}
