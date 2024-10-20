export class Hex {
    static readonly Background = '#F0F0F0';
    static readonly Error = '#B41C2B';
    static readonly Information = '#388CFA';
    static readonly Neutral = '#FFFFFF';
    static readonly Primary = '#388CFA';
    static readonly Secondary = '#38BC64';
    static readonly Success = '#009F42';
    static readonly Tertiary = '#BC3890';
    static readonly Warning = '#CC8800';
}

export class Factor {
    static readonly Regular = 0.85;
    static readonly Neutral = 0.95;
}

export class Shades {
    static readonly Full = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, -10, -20, -30, -40, -50, -60, -70, -80, -90, -100];
    static readonly FullPositive = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100];
    static readonly Regular = [20, 40, 60, 80, 100, -20, -40, -60, -80, -100];
    static readonly Information = [30, 60, 90, -30, -60, -90];
}

export class Colors {
    static readonly Colors: string[] = [
        'background',
        'error',
        'information',
        'neutral',
        'primary',
        'secondary',
        'success',
        'tertiary',
        'warning'
    ]

    static readonly BackgroundDependant: string[] = [
        '--sl-background-border',
        '--sl-background-hover',    
    ]
}



