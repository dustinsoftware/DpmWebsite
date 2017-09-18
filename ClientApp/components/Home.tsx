import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    state = {
        isOnline: navigator.onLine
    }

    refreshOnlineStatus = () => this.setState({ isOnline: navigator.onLine });

    componentDidMount() {
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
            <h4><a href="//twitter.com/dustinsoftware">Twitter</a> &middot; <a href="https://stackoverflow.com/cv/dustinmasters">Stack Overflow Careers</a></h4>
            <p>Hi there! I build software. This page is running on a Raspberry PI 3, using Docker and ASP.NET Core. I use Twitter occasionally, you can follow me <a href="//twitter.com/dustinsoftware">@dustinsoftware</a>. This website is a playground for different experiments.</p>
            <h4>It looks like you are currently {this.state.isOnline ? 'online 🌞' : 'offline! 🌚'}</h4>
            <p>This uses a service worker to cache pages and assets locally. Turn off your wifi and reload this page!</p>
        </div>;
    }
}
