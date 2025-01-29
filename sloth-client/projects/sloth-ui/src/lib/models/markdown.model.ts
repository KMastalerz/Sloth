export interface MarkdownSelectionDetails {
    startIndex: number;
    endIndex: number;
    selectionText: string,
    beforeText: string,
    afterText: string,
    isSingleIndex: boolean;
    startLine: number;
    endLine: number;
}

  
export enum MarkdownMarker {
    Quote,
    Code,
    Bold,
    Italic,
    Strikethrough,
    OList,
    UList
}
  