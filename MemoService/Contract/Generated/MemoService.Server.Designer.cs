//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.

//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bolt.Server;
using Contract;
using Contract.Parameters;
using Owin;


namespace Contract
{
    internal partial class MemoServiceInvoker : Bolt.Server.ContractInvoker<Contract.MemoServiceDescriptor>
    {
        public override void Init()
        {
            AddAction(Descriptor.Login, MemoService_Login);
            AddAction(Descriptor.AddMemo, MemoService_AddMemo);
            AddAction(Descriptor.RemoveMemo, MemoService_RemoveMemo);
            AddAction(Descriptor.GetAllMemos, MemoService_GetAllMemos);
            AddAction(Descriptor.Logoff, MemoService_Logoff);

            base.Init();
        }

        protected virtual async Task MemoService_Login(Bolt.Server.ServerActionContext context)
        {
            var parameters = await DataHandler.ReadParametersAsync<Contract.Parameters.LoginParameters>(context);
            var instance = InstanceProvider.GetInstance<IMemoService>(context);
            try
            {
                instance.Login(parameters.UserName);
                await ResponseHandler.Handle(context);
                InstanceProvider.ReleaseInstance(context, instance, null);
            }
            catch (Exception e)
            {
                InstanceProvider.ReleaseInstance(context, instance, e);
                throw;
            }
        }

        protected virtual async Task MemoService_AddMemo(Bolt.Server.ServerActionContext context)
        {
            var parameters = await DataHandler.ReadParametersAsync<Contract.Parameters.AddMemoParameters>(context);
            var instance = InstanceProvider.GetInstance<IMemoService>(context);
            try
            {
                instance.AddMemo(parameters.Memo);
                await ResponseHandler.Handle(context);
                InstanceProvider.ReleaseInstance(context, instance, null);
            }
            catch (Exception e)
            {
                InstanceProvider.ReleaseInstance(context, instance, e);
                throw;
            }
        }

        protected virtual async Task MemoService_RemoveMemo(Bolt.Server.ServerActionContext context)
        {
            var parameters = await DataHandler.ReadParametersAsync<Contract.Parameters.RemoveMemoParameters>(context);
            var instance = InstanceProvider.GetInstance<IMemoService>(context);
            try
            {
                instance.RemoveMemo(parameters.Memo);
                await ResponseHandler.Handle(context);
                InstanceProvider.ReleaseInstance(context, instance, null);
            }
            catch (Exception e)
            {
                InstanceProvider.ReleaseInstance(context, instance, e);
                throw;
            }
        }

        protected virtual async Task MemoService_GetAllMemos(Bolt.Server.ServerActionContext context)
        {
            var instance = InstanceProvider.GetInstance<IMemoService>(context);
            try
            {
                var result = instance.GetAllMemos();
                await ResponseHandler.Handle(context, result);
                InstanceProvider.ReleaseInstance(context, instance, null);
            }
            catch (Exception e)
            {
                InstanceProvider.ReleaseInstance(context, instance, e);
                throw;
            }
        }

        protected virtual async Task MemoService_Logoff(Bolt.Server.ServerActionContext context)
        {
            var instance = InstanceProvider.GetInstance<IMemoService>(context);
            try
            {
                instance.Logoff();
                await ResponseHandler.Handle(context);
                InstanceProvider.ReleaseInstance(context, instance, null);
            }
            catch (Exception e)
            {
                InstanceProvider.ReleaseInstance(context, instance, e);
                throw;
            }
        }
    }
}

namespace Bolt.Server
{
    internal static partial class MemoServiceInvokerExtensions
    {
        public static IAppBuilder UseMemoService(this IAppBuilder app, Contract.IMemoService instance, ServerConfiguration configuration = null)
        {
            return app.UseMemoService(new StaticInstanceProvider(instance), configuration);
        }

        public static IAppBuilder UseMemoService<TImplementation>(this IAppBuilder app, ServerConfiguration configuration = null) where TImplementation: Contract.IMemoService, new()
        {
            return app.UseMemoService(new InstanceProvider<TImplementation>(), configuration);
        }

        public static IAppBuilder UseStateFullMemoService<TImplementation>(this IAppBuilder app, string sessionHeader = null, TimeSpan? sessionTimeout = null, ServerConfiguration configuration = null) where TImplementation: Contract.IMemoService, new()
        {
            var initSessionAction = MemoServiceDescriptor.Default.Login;
            var closeSessionAction = MemoServiceDescriptor.Default.Logoff;
            return app.UseMemoService(new StateFullInstanceProvider<TImplementation>(initSessionAction, closeSessionAction, sessionHeader ?? app.GetBolt().Configuration.SessionHeader, sessionTimeout ?? app.GetBolt().Configuration.StateFullInstanceLifetime), configuration);
        }

        public static IAppBuilder UseStateFullMemoService<TImplementation>(this IAppBuilder app, ActionDescriptor initInstanceAction, ActionDescriptor releaseInstanceAction, string sessionHeader = null, TimeSpan? sessionTimeout = null, ServerConfiguration configuration = null) where TImplementation: Contract.IMemoService, new()
        {
            return app.UseMemoService(new StateFullInstanceProvider<TImplementation>(initInstanceAction, releaseInstanceAction, sessionHeader ?? app.GetBolt().Configuration.SessionHeader, sessionTimeout ?? app.GetBolt().Configuration.StateFullInstanceLifetime), configuration);
        }

        public static IAppBuilder UseMemoService(this IAppBuilder app, IInstanceProvider instanceProvider, ServerConfiguration configuration = null)
        {
            var boltExecutor = app.GetBolt();
            var invoker = new Contract.MemoServiceInvoker();
            invoker.Init(configuration ?? boltExecutor.Configuration);
            invoker.InstanceProvider = instanceProvider;
            boltExecutor.Add(invoker);

            return app;
        }
    }
}