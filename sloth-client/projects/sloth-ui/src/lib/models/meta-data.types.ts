export type GridMetadata = {
    columns?: number;
    rows?: number;
    columnsRatio?: (string | number)[];
    rowsRatio?: (string | number)[];
    gridSpans?: GridSpan[];
};

export type GridSpan = {
    type: 'column' | 'row';
    id: number;
    spanFrom: number;
    spanTo: number;
}

export type ButtonInnerType = {
    style?: 'flat' | 'neutral' | 'primary' | 'secondary' | 'tertiary' | 'error' | null;
    type?: 'icon' | 'text' | 'icon-text'| null;
    size?: 'small' | 'medium' | 'large'| null;
}

export type LinkMetadata = {
    tooltipPlacement: 'above' | 'below' | 'left' | 'right';
    counterSubject?: string,
    warningCount?: number,
    errorCount?: number
}

export type ButtonMetadata = {
    tooltipPlacement: 'above' | 'below' | 'left' | 'right';
    counterSubject?: string,
    warningCount?: number,
    errorCount?: number
}