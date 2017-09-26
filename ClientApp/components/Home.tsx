import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    state = {
        isOnline: navigator.onLine,
        visitorCount: 0
    }

    refreshOnlineStatus = () => {
        this.setState({ isOnline: navigator.onLine });

        if (navigator.onLine) {
            fetch('/api/log')
                .then((response) => response.json())
                .then(response => this.setState({ visitorCount: response.visitors }));
        }
    }

    componentDidMount() {
        this.refreshOnlineStatus();
        window.addEventListener('online', this.refreshOnlineStatus);
        window.addEventListener('offline', this.refreshOnlineStatus);
    }

    componentWillUnmount() {
        window.removeEventListener('online', this.refreshOnlineStatus);
        window.removeEventListener('offline', this.refreshOnlineStatus);
    }

    public render() {
        return <div>
            <h1>Dustin Software, Inc.</h1>
            <h4><a href="//twitter.com/dustinsoftware">Twitter</a> &middot; <a href="https://github.com/dustinsoftware">Github</a> &middot; <a href="https://stackoverflow.com/cv/dustinmasters">Stack Overflow Careers</a></h4>
            <p>Hi there! I build software. This page is running on a Raspberry PI 3, using Docker and ASP.NET Core. I use Twitter occasionally, you can follow me <a href="//twitter.com/dustinsoftware">@dustinsoftware</a>. This website is a playground for different experiments.</p>
            <h4>It looks like you are currently {this.state.isOnline ? 'online 🌞' : 'offline! 🌚'}</h4>
            <p>This uses a service worker to cache pages and assets locally. Turn off your wifi and reload this page!</p>
            {this.state.visitorCount > 0 && (
                <p>What's a personal website without a visitor counter! Hello, visitor #{this.state.visitorCount} 👋</p>
            )}
        </div>;
    }
}
