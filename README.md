# CrossOverlay
CrossOverlay is my cringy attempt into WPF'n'C# programming w/o any actuall skills and knowledge base. My goal was to achieve a working prototype of an overlay transparent click-through application displaying given image at the absolute center of the screen so it could be used as crosshair overlay for games.

So far with a bit less than a 40 minutes of work I've got myself pretty good (imo) result which can be used as it was intended to be.

## Current capabilities
At the moment application doesn't support any commands or shortcuts, doesn't have any pretty UI or easy to pick up config system (that will be covered just now), but appliction already can be configured via `App.config`. Few notes about configuration process.

### Configuration
By default, application is bundled with crosshair image called `xhair.png` (it should be placed near `.exe` file). You can replace it with your own, but I suggest you insted to drop your crosshairs into the folder and change `crosshairRelPath` parameter in `App.config` instead. Keep in mind, that this paramter describes relative path from executable itself, not absolute path (I should have changed that earlier, meh).

Also you might want to change a few more parameters so that your crosshair will fit you nice and easy.

1. `scaleMultiplier` - does literally what it says and multiplies crosshair's size. Positive float value. Default `1,0` (equals `20px`).
2. `crosshairOpacity` - obviously enough, opacity. Float value between `0,0` and `1,0`.
3. `marginXXXX` - margin which is used to offset crosshair from screen center. Might be useful for windowed mode. Double value.

## Further plans
I'm looking forward to add a few useful things, so that useless crap of code could be actually called a finished (and a bit useful) application.

So, prioiritized list looking something like that for now:

[ ] Handle possible exceptions
[ ] Add separated configuration presets
[ ] Simple UI to control settings
[ ] Shortcuts support