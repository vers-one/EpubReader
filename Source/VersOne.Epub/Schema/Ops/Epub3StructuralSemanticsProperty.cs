﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Structural semantics property used within EPUB 3 navigation or SMIL document.</para>
    /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/" /> for more information.</para>
    /// </summary>
    public enum Epub3StructuralSemanticsProperty
    {
        /// <summary>
        /// <para>A section that introduces the work, often consisting of a marketing image, the title, author and publisher, and select quotes and reviews.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#cover" /> for more information.</para>
        /// </summary>
        COVER = 1,

        /// <summary>
        /// <para>Preliminary material to the main content of a publication, such as tables of contents, dedications, etc.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#frontmatter" /> for more information.</para>
        /// </summary>
        FRONTMATTER,

        /// <summary>
        /// <para>The main content of a publication.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#bodymatter" /> for more information.</para>
        /// </summary>
        BODYMATTER,

        /// <summary>
        /// <para>Ancillary material occurring after the main content of a publication, such as indices, appendices, etc.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#backmatter" /> for more information.</para>
        /// </summary>
        BACKMATTER,

        /// <summary>
        /// <para>A component of a collection.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#volume" /> for more information.</para>
        /// </summary>
        VOLUME,

        /// <summary>
        /// <para>A major structural division in a work that contains a set of related sections dealing with a particular subject, narrative arc or similar encapsulated theme.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#part" /> for more information.</para>
        /// </summary>
        PART,

        /// <summary>
        /// <para>A major thematic section of content in a work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#chapter" /> for more information.</para>
        /// </summary>
        CHAPTER,

        /// <summary>
        /// <para>A major sub-division of a chapter.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#subchapter" /> for more information.</para>
        /// </summary>
        SUBCHAPTER,

        /// <summary>
        /// <para>A major structural division that may also appear as a substructure of a part (esp. in legislation).</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#division" /> for more information.</para>
        /// </summary>
        DIVISION,

        /// <summary>
        /// <para>A short summary of the principle ideas, concepts and conclusions of the work, or of a section or excerpt within it.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#abstract" /> for more information.</para>
        /// </summary>
        ABSTRACT,

        /// <summary>
        /// <para>An introductory section that precedes the work, typically not written by the author of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#foreword" /> for more information.</para>
        /// </summary>
        FOREWORD,

        /// <summary>
        /// <para>An introductory section that precedes the work, typically written by the author of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#preface" /> for more information.</para>
        /// </summary>
        PREFACE,

        /// <summary>
        /// <para>An introductory section that sets the background to a work, typically part of the narrative.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#prologue" /> for more information.</para>
        /// </summary>
        PROLOGUE,

        /// <summary>
        /// <para>A preliminary section that typically introduces the scope or nature of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#introduction" /> for more information.</para>
        /// </summary>
        INTRODUCTION,

        /// <summary>
        /// <para>A section in the beginning of the work, typically containing introductory and/or explanatory prose regarding the scope or nature of the work's content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#preamble" /> for more information.</para>
        /// </summary>
        PREAMBLE,

        /// <summary>
        /// <para>A concluding section or statement that summarizes the work or wraps up the narrative.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#conclusion" /> for more information.</para>
        /// </summary>
        CONCLUSION,

        /// <summary>
        /// <para>A concluding section of narrative that wraps up or comments on the actions and events of the work, typically from a future perspective.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#epilogue" /> for more information.</para>
        /// </summary>
        EPILOGUE,

        /// <summary>
        /// <para>
        /// A closing statement from the author or a person of importance, typically providing insight into how the content came to be written, its significance,
        /// or related events that have transpired since its timeline.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#afterword" /> for more information.</para>
        /// </summary>
        AFTERWORD,

        /// <summary>
        /// <para>A quotation set at the start of the work or a section that establishes the theme or sets the mood.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#epigraph" /> for more information.</para>
        /// </summary>
        EPIGRAPH,

        /// <summary>
        /// <para>
        /// A navigational aid that provides an ordered list of links to the major sectional headings in the content.
        /// A table of contents may cover an entire work, or only a smaller section of it.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#toc-1" /> for more information.</para>
        /// </summary>
        TOC,

        /// <summary>
        /// <para>An abridged version of the table of contents.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#toc-brief" /> for more information.</para>
        /// </summary>
        TOC_BRIEF,

        /// <summary>
        /// <para>A collection of references to well-known/recurring components within the publication.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#landmarks" /> for more information.</para>
        /// </summary>
        LANDMARKS,

        /// <summary>
        /// <para>A listing of audio clips included in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#loa" /> for more information.</para>
        /// </summary>
        LOA,

        /// <summary>
        /// <para>A listing of illustrations included in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#loi" /> for more information.</para>
        /// </summary>
        LOI,

        /// <summary>
        /// <para>A listing of tables included in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#lot" /> for more information.</para>
        /// </summary>
        LOT,

        /// <summary>
        /// <para>A listing of video clips included in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#lov" /> for more information.</para>
        /// </summary>
        LOV,

        /// <summary>
        /// <para>A section of supplemental information located after the primary content that informs the content but is not central to it.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#appendix" /> for more information.</para>
        /// </summary>
        APPENDIX,

        /// <summary>
        /// <para>A short section of production notes particular to the edition (e.g., describing the typeface used), often located at the end of a work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#colophon" /> for more information.</para>
        /// </summary>
        COLOPHON,

        /// <summary>
        /// <para>A collection of credits.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#credits" /> for more information.</para>
        /// </summary>
        CREDITS,

        /// <summary>
        /// <para>A collection of keywords.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#keywords" /> for more information.</para>
        /// </summary>
        KEYWORDS,

        /// <summary>
        /// <para>A navigational aid that provides a detailed list of links to key subjects, names and other important topics covered in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index" /> for more information.</para>
        /// </summary>
        INDEX,

        /// <summary>
        /// <para>Narrative or other content to assist users in using the index.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-headnotes" /> for more information.</para>
        /// </summary>
        INDEX_HEADNOTES,

        /// <summary>
        /// <para>List of symbols, abbreviations or special formatting used in the index, and their meanings.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-legend" /> for more information.</para>
        /// </summary>
        INDEX_LEGEND,

        /// <summary>
        /// <para>Collection of consecutive main entries that share a common characteristic, for example the starting letter of the main entries.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-group" /> for more information.</para>
        /// </summary>
        INDEX_GROUP,

        /// <summary>
        /// <para>Collection of consecutive main entries or subentries.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-entry-list" /> for more information.</para>
        /// </summary>
        INDEX_ENTRY_LIST,

        /// <summary>
        /// <para>One term with any attendant subentries, locators, cross references, and/or editorial note.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-entry" /> for more information.</para>
        /// </summary>
        INDEX_ENTRY,

        /// <summary>
        /// <para>Word, phrase, string, glyph or image representing the indexable content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-term" /> for more information.</para>
        /// </summary>
        INDEX_TERM,

        /// <summary>
        /// <para>Editorial note pertaining to a single entry.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-editor-note" /> for more information.</para>
        /// </summary>
        INDEX_EDITOR_NOTE,

        /// <summary>
        /// <para>A reference to an occurrence of the indexed content in the publication.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-locator" /> for more information.</para>
        /// </summary>
        INDEX_LOCATOR,

        /// <summary>
        /// <para>Collection of sequential locators or locator ranges.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-locator-list" /> for more information.</para>
        /// </summary>
        INDEX_LOCATOR_LIST,

        /// <summary>
        /// <para>A pair of locators that connects a term to a range of content rather than a single point.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-locator-range" /> for more information.</para>
        /// </summary>
        INDEX_LOCATOR_RANGE,

        /// <summary>
        /// <para>Reference from one term to one or more preferred terms or term categories in an index (analogous to "See xxx").</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-xref-preferred" /> for more information.</para>
        /// </summary>
        INDEX_XREF_PREFERRED,

        /// <summary>
        /// <para>Reference from one term to one or more related terms or term categories in an index (analogous to "See also xxx").</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-xref-related" /> for more information.</para>
        /// </summary>
        INDEX_XREF_RELATED,

        /// <summary>
        /// <para>Word, phrase, string, glyph or image representing a category of terms in the index.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-term-category" /> for more information.</para>
        /// </summary>
        INDEX_TERM_CATEGORY,

        /// <summary>
        /// <para>Wrapper for a list of the term categories belonging to an index.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#index-term-categories" /> for more information.</para>
        /// </summary>
        INDEX_TERM_CATEGORIES,

        /// <summary>
        /// <para>A brief dictionary of new, uncommon or specialized terms used in the content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#glossary" /> for more information.</para>
        /// </summary>
        GLOSSARY,

        /// <summary>
        /// <para>A glossary term.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#glossterm" /> for more information.</para>
        /// </summary>
        GLOSSTERM,

        /// <summary>
        /// <para>The definition of a term in a glossary.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#glossdef" /> for more information.</para>
        /// </summary>
        GLOSSDEF,

        /// <summary>
        /// <para>A list of external references cited in the work, which may be to print or digital sources.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#bibliography" /> for more information.</para>
        /// </summary>
        BIBLIOGRAPHY,

        /// <summary>
        /// <para>
        /// A single reference to an external source in a bibliography.
        /// A biblioentry typically provides more detailed information than its reference(s) in the content (e.g., full title, author(s), publisher, publication date, etc.).
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#biblioentry" /> for more information.</para>
        /// </summary>
        BIBLIOENTRY,

        /// <summary>
        /// <para>The title page of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#titlepage" /> for more information.</para>
        /// </summary>
        TITLEPAGE,

        /// <summary>
        /// <para>The half title page of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#halftitlepage" /> for more information.</para>
        /// </summary>
        HALFTITLEPAGE,

        /// <summary>
        /// <para>The copyright page of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#copyright-page" /> for more information.</para>
        /// </summary>
        COPYRIGHT_PAGE,

        /// <summary>
        /// <para>Marketing section used to list related publications.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#seriespage" /> for more information.</para>
        /// </summary>
        SERIESPAGE,

        /// <summary>
        /// <para>A section or statement that acknowledges significant contributions by persons, organizations, governments and other entities to the realization of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#acknowledgments" /> for more information.</para>
        /// </summary>
        ACKNOWLEDGMENTS,

        /// <summary>
        /// <para>Information relating to the publication or distribution of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#imprint" /> for more information.</para>
        /// </summary>
        IMPRINT,

        /// <summary>
        /// <para>A formal statement authorizing the publication of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#imprimatur" /> for more information.</para>
        /// </summary>
        IMPRIMATUR,

        /// <summary>
        /// <para>A list of contributors to the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#contributors" /> for more information.</para>
        /// </summary>
        CONTRIBUTORS,

        /// <summary>
        /// <para>Acknowledgments of previously published parts of the work, illustration credits, and permission to quote from copyrighted material.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#other-credits" /> for more information.</para>
        /// </summary>
        OTHER_CREDITS,

        /// <summary>
        /// <para>A set of corrections discovered after initial publication of the work, sometimes referred to as corrigenda.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#errata" /> for more information.</para>
        /// </summary>
        ERRATA,

        /// <summary>
        /// <para>An inscription at the front of the work, typically addressed in tribute to one or more persons close to the author.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#dedication" /> for more information.</para>
        /// </summary>
        DEDICATION,

        /// <summary>
        /// <para>A record of changes made to a work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#revision-history" /> for more information.</para>
        /// </summary>
        REVISION_HISTORY,

        /// <summary>
        /// <para>A detailed analysis of a specific topic.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#case-study" /> for more information.</para>
        /// </summary>
        CASE_STUDY,

        /// <summary>
        /// <para>Helpful information that clarifies some aspect of the content or assists in its comprehension.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#help" /> for more information.</para>
        /// </summary>
        HELP,

        /// <summary>
        /// <para>Content, both textual and graphical, that is offset in the margin.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#marginalia" /> for more information.</para>
        /// </summary>
        MARGINALIA,

        /// <summary>
        /// <para>Notifies the user of consequences that might arise from an action or event. Examples include warnings, cautions and dangers.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#notice" /> for more information.</para>
        /// </summary>
        NOTICE,

        /// <summary>
        /// <para>A distinctively placed or highlighted quotation from the current content designed to draw attention to a topic or highlight a key point.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#pullquote" /> for more information.</para>
        /// </summary>
        PULLQUOTE,

        /// <summary>
        /// <para>Secondary or supplementary content, typically formatted as an inset or box.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#sidebar" /> for more information.</para>
        /// </summary>
        SIDEBAR,

        /// <summary>
        /// <para>Helpful information that clarifies some aspect of the content or assists in its comprehension.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#tip" /> for more information.</para>
        /// </summary>
        TIP,

        /// <summary>
        /// <para>A warning.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#warning" /> for more information.</para>
        /// </summary>
        WARNING,

        /// <summary>
        /// <para>The title appearing on the first page of a work or immediately before the text.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#halftitle" /> for more information.</para>
        /// </summary>
        HALFTITLE,

        /// <summary>
        /// <para>The full title of the work, either simple, in which case it is identical to title, or compound, in which case it consists of a title and a subtitle.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#fulltitle" /> for more information.</para>
        /// </summary>
        FULLTITLE,

        /// <summary>
        /// <para>The title of the work as displayed on the work's cover.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#covertitle" /> for more information.</para>
        /// </summary>
        COVERTITLE,

        /// <summary>
        /// <para>The primary name of a document component, such as a list, table or figure.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#title" /> for more information.</para>
        /// </summary>
        TITLE,

        /// <summary>
        /// <para>An explanatory or alternate title for the work, or a section or component within it.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#subtitle" /> for more information.</para>
        /// </summary>
        SUBTITLE,

        /// <summary>
        /// <para>The text label that precedes an ordinal in a component title (e.g., 'Chapter', 'Part', 'Figure', 'Table').</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#label" /> for more information.</para>
        /// </summary>
        LABEL,

        /// <summary>
        /// <para>An ordinal specifier for a component in a sequence of components (e.g., '1', 'IV', 'B-1').</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#ordinal" /> for more information.</para>
        /// </summary>
        ORDINAL,

        /// <summary>
        /// <para>A structurally insignificant heading that does not contribute to the hierarchical structure of the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#bridgehead" /> for more information.</para>
        /// </summary>
        BRIDGEHEAD,

        /// <summary>
        /// <para>An explicit designation or description of a learning objective or a reference to an explicit learning objective.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-objective" /> for more information.</para>
        /// </summary>
        LEARNING_OBJECTIVE,

        /// <summary>
        /// <para>A collection of learning objectives.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-objectives" /> for more information.</para>
        /// </summary>
        LEARNING_OBJECTIVES,

        /// <summary>
        /// <para>The understanding or ability a student is expected to achieve as a result of a lesson or activity.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-outcome" /> for more information.</para>
        /// </summary>
        LEARNING_OUTCOME,

        /// <summary>
        /// <para>A collection of learning outcomes.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-outcomes" /> for more information.</para>
        /// </summary>
        LEARNING_OUTCOMES,

        /// <summary>
        /// <para>A resource provided to enhance learning, or a reference to such a resource.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-resource" /> for more information.</para>
        /// </summary>
        LEARNING_RESOURCE,

        /// <summary>
        /// <para>A collection of learning resources.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-resources" /> for more information.</para>
        /// </summary>
        LEARNING_RESOURCES,

        /// <summary>
        /// <para>A formal set of expectations or requirements typically issued by a government or a standards body.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-standard" /> for more information.</para>
        /// </summary>
        LEARNING_STANDARD,

        /// <summary>
        /// <para>A collection of learning standards.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#learning-standards" /> for more information.</para>
        /// </summary>
        LEARNING_STANDARDS,

        /// <summary>
        /// <para>The component of a self-assessment problem that provides the answer to the question.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#answer" /> for more information.</para>
        /// </summary>
        ANSWER,

        /// <summary>
        /// <para>A collection of answers.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#answers" /> for more information.</para>
        /// </summary>
        ANSWERS,

        /// <summary>
        /// <para>A test, quiz, or other activity that helps measure a student's understanding of what is being taught.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#assessment" /> for more information.</para>
        /// </summary>
        ASSESSMENT,

        /// <summary>
        /// <para>A collection of assessments.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#assessments" /> for more information.</para>
        /// </summary>
        ASSESSMENTS,

        /// <summary>
        /// <para>Instruction to the reader based on the result of an assessment.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#feedback" /> for more information.</para>
        /// </summary>
        FEEDBACK,

        /// <summary>
        /// <para>A problem that requires the reader to input a text answer to complete a sentence, statement or similar.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#fill-in-the-blank-problem" /> for more information.</para>
        /// </summary>
        FILL_IN_THE_BLANK_PROBLEM,

        /// <summary>
        /// <para>A problem with a free-form solution.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#general-problem" /> for more information.</para>
        /// </summary>
        GENERAL_PROBLEM,

        /// <summary>
        /// <para>A section of content structured as a series of questions and answers, such as an interview or list of frequently asked questions.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#qna" /> for more information.</para>
        /// </summary>
        QNA,

        /// <summary>
        /// <para>A problem that requires the reader to match the contents of one list with the corresponding items in another list.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#match-problem" /> for more information.</para>
        /// </summary>
        MATCH_PROBLEM,

        /// <summary>
        /// <para>A problem with a set of potential answers to choose from ‒ some, all or none of which may be correct.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#multiple-choice-problem" /> for more information.</para>
        /// </summary>
        MULTIPLE_CHOICE_PROBLEM,

        /// <summary>
        /// <para>A review exercise or sample.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#practice" /> for more information.</para>
        /// </summary>
        PRACTICE,

        /// <summary>
        /// <para>The component of a self-assessment problem that identifies the question to be solved.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#question" /> for more information.</para>
        /// </summary>
        QUESTION,

        /// <summary>
        /// <para>A collection of practices.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#practices" /> for more information.</para>
        /// </summary>
        PRACTICES,

        /// <summary>
        /// <para>A problem with either a true or false answer.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#true-false-problem" /> for more information.</para>
        /// </summary>
        TRUE_FALSE_PROBLEM,

        /// <summary>
        /// <para>An individual frame, or drawing.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#panel" /> for more information.</para>
        /// </summary>
        PANEL,

        /// <summary>
        /// <para>A group of panels (e.g., a strip).</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#panel-group" /> for more information.</para>
        /// </summary>
        PANEL_GROUP,

        /// <summary>
        /// <para>An area in a comic panel that contains the words, spoken or thought, of a character.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#balloon" /> for more information.</para>
        /// </summary>
        BALLOON,

        /// <summary>
        /// <para>An area of text in a comic panel. Used to represent titles, narrative text, character dialogue (inside a balloon or not) and similar.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#text-area" /> for more information.</para>
        /// </summary>
        TEXT_AREA,

        /// <summary>
        /// <para>An area of text in a comic panel that represents a sound.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#sound-area" /> for more information.</para>
        /// </summary>
        SOUND_AREA,

        /// <summary>
        /// <para>Explanatory information about passages in the work.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#annotation" /> for more information.</para>
        /// </summary>
        ANNOTATION,

        /// <summary>
        /// <para>
        /// A note. This property does not carry spatial positioning semantics, as do the footnote and endnote properties.
        /// It can be used to identify footnotes, endnotes, marginal notes, inline notes, and similar when legacy naming conventions are not desired.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#note" /> for more information.</para>
        /// </summary>
        NOTE,

        /// <summary>
        /// <para>Ancillary information, such as a citation or commentary, that provides additional context to a referenced passage of text.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#footnote" /> for more information.</para>
        /// </summary>
        FOOTNOTE,

        /// <summary>
        /// <para>One of a collection of notes that occur at the end of a work, or a section within it, that provides additional context to a referenced passage of text.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#endnote" /> for more information.</para>
        /// </summary>
        ENDNOTE,

        /// <summary>
        /// <para>A note appearing in the rear (backmatter) of the work, or at the end of a section.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#rearnote" /> for more information.</para>
        /// </summary>
        REARNOTE,

        /// <summary>
        /// <para>A collection of footnotes.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#footnotes" /> for more information.</para>
        /// </summary>
        FOOTNOTES,

        /// <summary>
        /// <para>A collection of notes at the end of a work or a section within it.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#endnotes" /> for more information.</para>
        /// </summary>
        ENDNOTES,

        /// <summary>
        /// <para>A collection of notes appearing at the rear (backmatter) of the work, or at the end of a section.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#rearnotes" /> for more information.</para>
        /// </summary>
        REARNOTES,

        /// <summary>
        /// <para>A reference to an annotation.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#annoref" /> for more information.</para>
        /// </summary>
        ANNOREF,

        /// <summary>
        /// <para>A reference to a bibliography entry.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#biblioref" /> for more information.</para>
        /// </summary>
        BIBLIOREF,

        /// <summary>
        /// <para>A reference to a glossary definition.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#glossref" /> for more information.</para>
        /// </summary>
        GLOSSREF,

        /// <summary>
        /// <para>A reference to a note, typically appearing as a superscripted number or symbol in the main body of text.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#noteref" /> for more information.</para>
        /// </summary>
        NOTEREF,

        /// <summary>
        /// <para>
        /// A link that allows the user to return to a related location in the content (e.g., from a footnote to its reference or from a glossary definition to where a term is used).
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#backlink" /> for more information.</para>
        /// </summary>
        BACKLINK,

        /// <summary>
        /// <para>
        /// An acknowledgment of the source of integrated content from third-party sources, such as photos. Typically identifies the creator, copyright and any restrictions on reuse.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#credit" /> for more information.</para>
        /// </summary>
        CREDIT,

        /// <summary>
        /// <para>A key word or phrase.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#keyword" /> for more information.</para>
        /// </summary>
        KEYWORD,

        /// <summary>
        /// <para>A phrase or sentence serving as an introductory summary of the containing paragraph.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#topic-sentence" /> for more information.</para>
        /// </summary>
        TOPIC_SENTENCE,

        /// <summary>
        /// <para>A phrase or sentence serving as a concluding summary of the containing paragraph.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#concluding-sentence" /> for more information.</para>
        /// </summary>
        CONCLUDING_SENTENCE,

        /// <summary>
        /// <para>A separator denoting the position before which a break occurs between two contiguous pages in a statically paginated version of the content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#pagebreak" /> for more information.</para>
        /// </summary>
        PAGEBREAK,

        /// <summary>
        /// <para>A navigational aid that provides a list of links to the pagebreaks in the content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#page-list" /> for more information.</para>
        /// </summary>
        PAGE_LIST,

        /// <summary>
        /// <para>A structure containing data or content laid out in tabular form.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#table" /> for more information.</para>
        /// </summary>
        TABLE,

        /// <summary>
        /// <para>A row of data or content in a tabular structure.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#table-row" /> for more information.</para>
        /// </summary>
        TABLE_ROW,

        /// <summary>
        /// <para>A single cell of tabular data or content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#table-cell" /> for more information.</para>
        /// </summary>
        TABLE_CELL,

        /// <summary>
        /// <para>A structure that contains an enumeration of related content items.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#list" /> for more information.</para>
        /// </summary>
        LIST,

        /// <summary>
        /// <para>A single item in an enumeration.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#list-item" /> for more information.</para>
        /// </summary>
        LIST_ITEM,

        /// <summary>
        /// <para>An illustration, diagram, photo, code listing or similar, referenced from the text of a work, and typically annotated with a title, caption and/or credits.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#figure" /> for more information.</para>
        /// </summary>
        FIGURE,

        /// <summary>
        /// <para>Secondary or supplementary content.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-ssv-11/#aside" /> for more information.</para>
        /// </summary>
        ASIDE,

        /// <summary>
        /// A structural semantics property which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class Epub3StructuralSemanticsPropertyParser
    {
        public static List<Epub3StructuralSemanticsProperty> ParsePropertyList(string stringValue)
        {
            return stringValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                Select(propertyString => ParseProperty(propertyString.Trim())).
                ToList();
        }

        public static Epub3StructuralSemanticsProperty ParseProperty(string stringValue)
        {
            if (String.IsNullOrEmpty(stringValue))
            {
                return Epub3StructuralSemanticsProperty.UNKNOWN;
            }
            return stringValue.ToLowerInvariant() switch
            {
                "cover" => Epub3StructuralSemanticsProperty.COVER,
                "frontmatter" => Epub3StructuralSemanticsProperty.FRONTMATTER,
                "bodymatter" => Epub3StructuralSemanticsProperty.BODYMATTER,
                "backmatter" => Epub3StructuralSemanticsProperty.BACKMATTER,
                "volume" => Epub3StructuralSemanticsProperty.VOLUME,
                "part" => Epub3StructuralSemanticsProperty.PART,
                "chapter" => Epub3StructuralSemanticsProperty.CHAPTER,
                "subchapter" => Epub3StructuralSemanticsProperty.SUBCHAPTER,
                "division" => Epub3StructuralSemanticsProperty.DIVISION,
                "abstract" => Epub3StructuralSemanticsProperty.ABSTRACT,
                "foreword" => Epub3StructuralSemanticsProperty.FOREWORD,
                "preface" => Epub3StructuralSemanticsProperty.PREFACE,
                "prologue" => Epub3StructuralSemanticsProperty.PROLOGUE,
                "introduction" => Epub3StructuralSemanticsProperty.INTRODUCTION,
                "preamble" => Epub3StructuralSemanticsProperty.PREAMBLE,
                "conclusion" => Epub3StructuralSemanticsProperty.CONCLUSION,
                "epilogue" => Epub3StructuralSemanticsProperty.EPILOGUE,
                "afterword" => Epub3StructuralSemanticsProperty.AFTERWORD,
                "epigraph" => Epub3StructuralSemanticsProperty.EPIGRAPH,
                "toc" => Epub3StructuralSemanticsProperty.TOC,
                "toc-brief" => Epub3StructuralSemanticsProperty.TOC_BRIEF,
                "landmarks" => Epub3StructuralSemanticsProperty.LANDMARKS,
                "loa" => Epub3StructuralSemanticsProperty.LOA,
                "loi" => Epub3StructuralSemanticsProperty.LOI,
                "lot" => Epub3StructuralSemanticsProperty.LOT,
                "lov" => Epub3StructuralSemanticsProperty.LOV,
                "appendix" => Epub3StructuralSemanticsProperty.APPENDIX,
                "colophon" => Epub3StructuralSemanticsProperty.COLOPHON,
                "credits" => Epub3StructuralSemanticsProperty.CREDITS,
                "keywords" => Epub3StructuralSemanticsProperty.KEYWORDS,
                "index" => Epub3StructuralSemanticsProperty.INDEX,
                "index-headnotes" => Epub3StructuralSemanticsProperty.INDEX_HEADNOTES,
                "index-legend" => Epub3StructuralSemanticsProperty.INDEX_LEGEND,
                "index-group" => Epub3StructuralSemanticsProperty.INDEX_GROUP,
                "index-entry-list" => Epub3StructuralSemanticsProperty.INDEX_ENTRY_LIST,
                "index-entry" => Epub3StructuralSemanticsProperty.INDEX_ENTRY,
                "index-term" => Epub3StructuralSemanticsProperty.INDEX_TERM,
                "index-editor-note" => Epub3StructuralSemanticsProperty.INDEX_EDITOR_NOTE,
                "index-locator" => Epub3StructuralSemanticsProperty.INDEX_LOCATOR,
                "index-locator-list" => Epub3StructuralSemanticsProperty.INDEX_LOCATOR_LIST,
                "index-locator-range" => Epub3StructuralSemanticsProperty.INDEX_LOCATOR_RANGE,
                "index-xref-preferred" => Epub3StructuralSemanticsProperty.INDEX_XREF_PREFERRED,
                "index-xref-related" => Epub3StructuralSemanticsProperty.INDEX_XREF_RELATED,
                "index-term-category" => Epub3StructuralSemanticsProperty.INDEX_TERM_CATEGORY,
                "index-term-categories" => Epub3StructuralSemanticsProperty.INDEX_TERM_CATEGORIES,
                "glossary" => Epub3StructuralSemanticsProperty.GLOSSARY,
                "glossterm" => Epub3StructuralSemanticsProperty.GLOSSTERM,
                "glossdef" => Epub3StructuralSemanticsProperty.GLOSSDEF,
                "bibliography" => Epub3StructuralSemanticsProperty.BIBLIOGRAPHY,
                "biblioentry" => Epub3StructuralSemanticsProperty.BIBLIOENTRY,
                "titlepage" => Epub3StructuralSemanticsProperty.TITLEPAGE,
                "halftitlepage" => Epub3StructuralSemanticsProperty.HALFTITLEPAGE,
                "copyright-page" => Epub3StructuralSemanticsProperty.COPYRIGHT_PAGE,
                "seriespage" => Epub3StructuralSemanticsProperty.SERIESPAGE,
                "acknowledgments" => Epub3StructuralSemanticsProperty.ACKNOWLEDGMENTS,
                "imprint" => Epub3StructuralSemanticsProperty.IMPRINT,
                "imprimatur" => Epub3StructuralSemanticsProperty.IMPRIMATUR,
                "contributors" => Epub3StructuralSemanticsProperty.CONTRIBUTORS,
                "other-credits" => Epub3StructuralSemanticsProperty.OTHER_CREDITS,
                "errata" => Epub3StructuralSemanticsProperty.ERRATA,
                "dedication" => Epub3StructuralSemanticsProperty.DEDICATION,
                "revision-history" => Epub3StructuralSemanticsProperty.REVISION_HISTORY,
                "case-study" => Epub3StructuralSemanticsProperty.CASE_STUDY,
                "help" => Epub3StructuralSemanticsProperty.HELP,
                "marginalia" => Epub3StructuralSemanticsProperty.MARGINALIA,
                "notice" => Epub3StructuralSemanticsProperty.NOTICE,
                "pullquote" => Epub3StructuralSemanticsProperty.PULLQUOTE,
                "sidebar" => Epub3StructuralSemanticsProperty.SIDEBAR,
                "tip" => Epub3StructuralSemanticsProperty.TIP,
                "warning" => Epub3StructuralSemanticsProperty.WARNING,
                "halftitle" => Epub3StructuralSemanticsProperty.HALFTITLE,
                "fulltitle" => Epub3StructuralSemanticsProperty.FULLTITLE,
                "covertitle" => Epub3StructuralSemanticsProperty.COVERTITLE,
                "title" => Epub3StructuralSemanticsProperty.TITLE,
                "subtitle" => Epub3StructuralSemanticsProperty.SUBTITLE,
                "label" => Epub3StructuralSemanticsProperty.LABEL,
                "ordinal" => Epub3StructuralSemanticsProperty.ORDINAL,
                "bridgehead" => Epub3StructuralSemanticsProperty.BRIDGEHEAD,
                "learning-objective" => Epub3StructuralSemanticsProperty.LEARNING_OBJECTIVE,
                "learning-objectives" => Epub3StructuralSemanticsProperty.LEARNING_OBJECTIVES,
                "learning-outcome" => Epub3StructuralSemanticsProperty.LEARNING_OUTCOME,
                "learning-outcomes" => Epub3StructuralSemanticsProperty.LEARNING_OUTCOMES,
                "learning-resource" => Epub3StructuralSemanticsProperty.LEARNING_RESOURCE,
                "learning-resources" => Epub3StructuralSemanticsProperty.LEARNING_RESOURCES,
                "learning-standard" => Epub3StructuralSemanticsProperty.LEARNING_STANDARD,
                "learning-standards" => Epub3StructuralSemanticsProperty.LEARNING_STANDARDS,
                "answer" => Epub3StructuralSemanticsProperty.ANSWER,
                "answers" => Epub3StructuralSemanticsProperty.ANSWERS,
                "assessment" => Epub3StructuralSemanticsProperty.ASSESSMENT,
                "assessments" => Epub3StructuralSemanticsProperty.ASSESSMENTS,
                "feedback" => Epub3StructuralSemanticsProperty.FEEDBACK,
                "fill-in-the-blank-problem" => Epub3StructuralSemanticsProperty.FILL_IN_THE_BLANK_PROBLEM,
                "general-problem" => Epub3StructuralSemanticsProperty.GENERAL_PROBLEM,
                "qna" => Epub3StructuralSemanticsProperty.QNA,
                "match-problem" => Epub3StructuralSemanticsProperty.MATCH_PROBLEM,
                "multiple-choice-problem" => Epub3StructuralSemanticsProperty.MULTIPLE_CHOICE_PROBLEM,
                "practice" => Epub3StructuralSemanticsProperty.PRACTICE,
                "question" => Epub3StructuralSemanticsProperty.QUESTION,
                "practices" => Epub3StructuralSemanticsProperty.PRACTICES,
                "true-false-problem" => Epub3StructuralSemanticsProperty.TRUE_FALSE_PROBLEM,
                "panel" => Epub3StructuralSemanticsProperty.PANEL,
                "panel-group" => Epub3StructuralSemanticsProperty.PANEL_GROUP,
                "balloon" => Epub3StructuralSemanticsProperty.BALLOON,
                "text-area" => Epub3StructuralSemanticsProperty.TEXT_AREA,
                "sound-area" => Epub3StructuralSemanticsProperty.SOUND_AREA,
                "annotation" => Epub3StructuralSemanticsProperty.ANNOTATION,
                "note" => Epub3StructuralSemanticsProperty.NOTE,
                "footnote" => Epub3StructuralSemanticsProperty.FOOTNOTE,
                "endnote" => Epub3StructuralSemanticsProperty.ENDNOTE,
                "rearnote" => Epub3StructuralSemanticsProperty.REARNOTE,
                "footnotes" => Epub3StructuralSemanticsProperty.FOOTNOTES,
                "endnotes" => Epub3StructuralSemanticsProperty.ENDNOTES,
                "rearnotes" => Epub3StructuralSemanticsProperty.REARNOTES,
                "annoref" => Epub3StructuralSemanticsProperty.ANNOREF,
                "biblioref" => Epub3StructuralSemanticsProperty.BIBLIOREF,
                "glossref" => Epub3StructuralSemanticsProperty.GLOSSREF,
                "noteref" => Epub3StructuralSemanticsProperty.NOTEREF,
                "backlink" => Epub3StructuralSemanticsProperty.BACKLINK,
                "credit" => Epub3StructuralSemanticsProperty.CREDIT,
                "keyword" => Epub3StructuralSemanticsProperty.KEYWORD,
                "topic-sentence" => Epub3StructuralSemanticsProperty.TOPIC_SENTENCE,
                "concluding-sentence" => Epub3StructuralSemanticsProperty.CONCLUDING_SENTENCE,
                "pagebreak" => Epub3StructuralSemanticsProperty.PAGEBREAK,
                "page-list" => Epub3StructuralSemanticsProperty.PAGE_LIST,
                "table" => Epub3StructuralSemanticsProperty.TABLE,
                "table-row" => Epub3StructuralSemanticsProperty.TABLE_ROW,
                "table-cell" => Epub3StructuralSemanticsProperty.TABLE_CELL,
                "list" => Epub3StructuralSemanticsProperty.LIST,
                "list-item" => Epub3StructuralSemanticsProperty.LIST_ITEM,
                "figure" => Epub3StructuralSemanticsProperty.FIGURE,
                "aside" => Epub3StructuralSemanticsProperty.ASIDE,
                _ => Epub3StructuralSemanticsProperty.UNKNOWN
            };
        }
    }
}
