# To create your own contributor pipeline:

* Duplicate the Parent Folder
* Rename your new pipelines (EX: NebbyPipeline -> MyPipeline)
    * Pipeline names must be unique, so make sure you use your actual developer name
* Click the Path Reference (Green TK logo)
    * On the Constant, paste the location of your R2ModMan development plugins folder.
    * Rename the path reference (EX: NebbyPath -> MyPath)
        * Path names must be unique, so make sure you use your actual developer name
* Click each pipeline
    * Replace the string ``<NebbyPath>`` with the name of your path reference (EX: ``<NebbyPath> -> <MyPath>)
        * The brackets are REQUIRED, otherwise thunderkit will just think its a regular path and will error on copy.