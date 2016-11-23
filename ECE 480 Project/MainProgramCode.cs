﻿using Polyglot.Core;
using System;
using System.Collections.Generic;
//using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ECE_480_Project
{
    class MainProgramCode
    {
        //static Language[] langs = new Language[3];
        //static ILangProcess[] processes = new ILangProcess[3];
        private const int MaxLength = 4096;
        private const int MaxPenalty = 300;

        private readonly Dictionary<string, Dictionary<string, int>> _availableLanguages;

        public MainProgramCode(Dictionary<string, Dictionary<string, int>> availableLanguages)
        {
            _availableLanguages = availableLanguages;
        }

        const string knownLanguagesFile = @"C:\Users\Admin\Desktop\detect\known_languages.txt";
        public Language[] MainProgramCode(string stringInput)
        {
            // Fast Brain Process
            // each FBP should inherant ILangProcess interface
            //Detect(stringInput, knownLanguagesFile);
            //processes[0] = new EnglishFBP(stringInput);
            //processes[1] = new EnglishFBP(stringInput);
            //processes[2] = new EnglishFBP(stringInput);

            // Diego: Thread all processes here for FBP & add runtime start/stops

            // return results here
            int score = 0;

            var text = stringInput; //read input

            var ngramBuilder = new NgramBuilder(MaxLength, true);

            var ngrams = ngramBuilder.Get(text); //create an ngram dictionary

            if (ngrams == null)
            {
                return null;
            }

            var shortestDistance = int.MaxValue;

            var probability = 0;

            string lowestScoringLanguage = null;

            foreach (var availableLanguage in _availableLanguages)
            {
                //calculate distance between language and ngrams

                var distance = 0;

                var probabilityHits = 0;

                foreach (var ngram in ngrams)
                {
                    if (availableLanguage.Value.ContainsKey(ngram.Key))
                    {
                        distance += ngram.Value - availableLanguage.Value[ngram.Key];

                        probabilityHits++;
                    }
                    else
                    {
                        distance += MaxPenalty;
                    }

                    if (distance > shortestDistance)
                    {
                        break;
                    }
                }

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    lowestScoringLanguage = availableLanguage.Key;
                    probability = probabilityHits;
                }
            }

            if (probability > 30)
                probability = 100;
            else if (probability < 30)
                probability = 80;

            score = probability;

            //return lowestScoringLanguage;
        }
            if (count > 0) { }
                // Slow Brain processes



            //return langs;
        }
    }
}
