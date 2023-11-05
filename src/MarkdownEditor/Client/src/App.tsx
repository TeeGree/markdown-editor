import React, { useEffect, useState } from 'react';
import classes from './App.module.scss';
import {
    MDXEditor,
    headingsPlugin,
    type MDXEditorMethods,
    quotePlugin,
    listsPlugin,
    thematicBreakPlugin,
    toolbarPlugin,
    UndoRedo,
    BoldItalicUnderlineToggles,
    DiffSourceToggleWrapper,
    diffSourcePlugin,
} from '@mdxeditor/editor';
import { fetchGetResponse, fetchPutResponse } from './helpers/fetchFunctions';
import '@mdxeditor/editor/style.css';
import { Button } from '@mui/material';

export const App: React.FC<Record<string, never>> = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [markdownFromFile, setMarkdownFromFile] = useState('');

    const getMarkdown = async () => {
        const result = await fetchGetResponse('FileAccess');

        const markdown: string = await result.text();

        setMarkdownFromFile(markdown);

        setIsLoading(false);
    };

    const saveMarkdown = async () => {
        const result = await fetchPutResponse('FileAccess/UpdateMarkdown', {
            markdown: ref.current?.getMarkdown(),
        });

        if (result.ok) {
            setIsLoading(true);

            await getMarkdown();
        }
    };

    useEffect(() => {
        setIsLoading(true);
        getMarkdown();
    }, []);

    // construct a ref to the editor
    const ref = React.useRef<MDXEditorMethods>(null);

    const getContent = () => {
        if (isLoading) {
            return 'Loading...';
        }

        return (
            <>
                <div className={classes.markdownEditorContainer}>
                    <MDXEditor
                        className={classes.markdownEditor}
                        ref={ref}
                        markdown={markdownFromFile}
                        plugins={[
                            diffSourcePlugin({
                                diffMarkdown: markdownFromFile,
                                viewMode: 'rich-text',
                            }),
                            headingsPlugin(),
                            quotePlugin(),
                            listsPlugin(),
                            thematicBreakPlugin(),
                            toolbarPlugin({
                                toolbarContents: () => (
                                    <>
                                        <DiffSourceToggleWrapper>
                                            <UndoRedo />
                                            <BoldItalicUnderlineToggles />
                                        </DiffSourceToggleWrapper>
                                    </>
                                ),
                            }),
                        ]}
                        onChange={console.log}
                    />
                    <Button
                        variant="contained"
                        color="success"
                        className={classes.save}
                        onClick={saveMarkdown}
                    >
                        Save markdown
                    </Button>
                </div>
            </>
        );
    };

    return <div className={classes.app}>{getContent()}</div>;
};
