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
    static readonly Neutral = 0.98;
}

export class Shades {
    static readonly Regular = [20, 40, 60, 80, 100, -20, -40, -60, -80, -100];
    static readonly Neutral = [20, 40, 60, 80, 100];
    static readonly Information = [30, 60, 90, -30, -60, -90];
}

export enum ThemeType {
    Regular, 
    Neutral, 
    Information
}

export interface Color {
	name: string;
	type: ThemeType;
	hex: Hex;
}

export class Colors {
    static readonly Colors: Color[] = [
        {
            name: 'background',
            type: ThemeType.Regular,
            hex: Hex.Background
        },
        {
            name: 'error',
            type: ThemeType.Information,
            hex: Hex.Error
        },
        {
            name: 'information',
            type: ThemeType.Information,
            hex: Hex.Information
        },
        {
            name: 'neutral',
            type: ThemeType.Neutral,
            hex: Hex.Neutral
        },
        {
            name: 'primary',
            type: ThemeType.Regular,
            hex: Hex.Primary
        },
        {
            name: 'secondary',
            type: ThemeType.Regular,
            hex: Hex.Secondary
        },
        {
            name: 'success',
            type: ThemeType.Information,
            hex: Hex.Success
        },
        {
            name: 'tertiary',
            type: ThemeType.Regular,
            hex: Hex.Tertiary
        },
        {
            name: 'warning',
            type: ThemeType.Information,
            hex: Hex.Warning
        }
    ]
}



