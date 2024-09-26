using MSU;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MSUTemplate
{
    //This is our mod's ContentPack provider, here's where the Assetbundle loading occurs, which allows our assets and mod
    //initialization to occurr inside the loading screen
    public class MSUTContent : IContentPackProvider
    {
        //We can just use or mod's GUID as the identifier
        public string identifier => MSUTMain.GUID;

        //public facing property to let other mods access our content pack in a read only fashion
        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(msuTemplateContentPack);

        //Our mod's actual content pack
        internal static ContentPack msuTemplateContentPack { get; } = new ContentPack();

        //These 3 fields are utilized by the mod to load our assets and initialize the mod.

        //This ParallelMultiStartCoroutine can be used to load assets BEFORE actual mod content intialization is made. 
        internal static ParallelMultiStartCoroutine _parallelPreLoadDispatchers = new ParallelMultiStartCoroutine();

        //This is an array of coroutine calls, your mod's content initialization (IE: utilizing MSU's modules for initializing your
        //content) happens here, its not parallel to avoid potential race conditions. This field is created inside the static constructor of MSUTContent
        private static Func<IEnumerator>[] _loadDispatchers;

        //This ParallelMultiStartCoroutine can be used to load assets AFTER our mod finishes content intialization.
        internal static ParallelMultiStartCoroutine _parallelPostLoadDispatchers = new ParallelMultiStartCoroutine();

        //This array of actions is used to populate "content" classes, An example in vanilla includes the RoR2Content.Items class, which
        //contains references to all the base game's ItemDefs.
        private static Action[] _fieldAssignDispatchers;
        private bool _initialized;

        /// <summary>
        /// This is called by the game and as such should not be called.
        /// </summary>
        //We're implementing the IContentPackProvider in an explicit format, this way we avoid the potentiality of an external mod from
        //calling these methods.
        IEnumerator IContentPackProvider.LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            if (_initialized)
                yield break;

            _initialized = true;
            var enumerator = MSUTAssets.Initialize(); //We initialize our assetbundles and await them.
            while (enumerator.MoveNext())
                yield return null;

            _parallelPreLoadDispatchers.Start(); //We call the pre load methods and await all of them.
            while (!_parallelPreLoadDispatchers.isDone) yield return null;

            //This is what loads and initializes our content, it'll automatically report progress back to our game, which will be used
            //during the loading screen.
            for (int i = 0; i < _loadDispatchers.Length; i++)
            {
                args.ReportProgress(Util.Remap(i + 1, 0f, _loadDispatchers.Length, 0.1f, 0.2f)); //report progress
                enumerator = _loadDispatchers[i](); //call method

                while (enumerator?.MoveNext() ?? false) yield return null; //await
            }

            _parallelPostLoadDispatchers.Start(); //We call the post load methods and await all of them
            while (!_parallelPostLoadDispatchers.isDone) yield return null;

            //This assigns our content to our desired static classes.
            for (int i = 0; i < _fieldAssignDispatchers.Length; i++)
            {
                args.ReportProgress(Util.Remap(i + 1, 0f, _fieldAssignDispatchers.Length, 0.95f, 0.99f));
                _fieldAssignDispatchers[i]();
            }
        }

        //We're implementing the IContentPackProvider in an explicit format, this way we avoid the potentiality of an external mod from
        //calling these methods.
        IEnumerator IContentPackProvider.GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(msuTemplateContentPack, args.output);
            args.ReportProgress(1f);
            yield return null;
        }

        //We're implementing the IContentPackProvider in an explicit format, this way we avoid the potentiality of an external mod from
        //calling these methods.
        IEnumerator IContentPackProvider.FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        //Add our content pack provider to the game's content pack system
        private void AddSelf(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }

        //We can use this method to create a Parallel coroutine that'll load multiple assets into the content pack, examples of this
        //would be loading and assigning the EntityStateConfiguration array and others
        private static IEnumerator LoadFromAssetBundles()
        {
            yield break;
        }

        //AsyncAssetLoad attributes can be used to call multiple coroutines at the same time that load assets, more info
        //can be found in its documentation.
        private IEnumerator CallAsyncAssetLoadAttributes()
        {
            var routine = AsyncAssetLoadAttribute.CreateCoroutineForMod(MSUTMain.instance);
            routine.Start();
            while (!routine.isDone)
                yield return null;
        }

        //Constructor for our content pack
        internal MSUTContent()
        {
            ContentManager.collectContentPackProviders += AddSelf; //Make sure we add our pack provider to the game's system
            _parallelPreLoadDispatchers.Add(CallAsyncAssetLoadAttributes);
        }

        static MSUTContent()
        {
            MSUTMain main = MSUTMain.instance; //get a direct reference to our plugin for ease of access
            _loadDispatchers = new Func<IEnumerator>[] //Create our array that will initialize our mod.
            {
                () => 
                {
                    //This is the basic syntax of utilizing a module from MSU for initializing our content, we call 
                    //AddProvider to let the module know we're adding new content. And also create a simple content
                    //provider utilizing the ContentUtil class.
                    ItemModule.AddProvider(main, ContentUtil.CreateGenericContentPieceProvider<ItemDef>(main, msuTemplateContentPack));

                    //Then we directly return the coroutine that initializes our items
                    return ItemModule.InitializeItems(main);
                },
                LoadFromAssetBundles
            };

            _fieldAssignDispatchers = new Action[]
            {
                () => ContentUtil.PopulateTypeFields(typeof(Items), msuTemplateContentPack.itemDefs),
            };
        }

        public static class Items
        {
            public static ItemDef ExampleItem;
        }
    }
}