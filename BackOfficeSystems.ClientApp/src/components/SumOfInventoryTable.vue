<template>
    <div>
        <md-table v-model="data" :md-sort.sync="currentSort" :md-sort-order.sync="currentSortOrder" :md-sort-fn="customSort" md-card>
            <md-table-toolbar>
                <h1 class="md-title">Sum of inventory</h1>
            </md-table-toolbar>

            <md-table-row slot="md-table-row" slot-scope="{ item }">
                <md-table-cell md-label="ID" md-numeric md-sort-by="brandId">{{ item.brandId }}</md-table-cell>
                <md-table-cell md-label="Name" md-sort-by="name">{{ item.name }}</md-table-cell>
                <md-table-cell md-label="Count" md-numeric md-sort-by="count">{{ item.count }}</md-table-cell>
            </md-table-row>
        </md-table>
    </div>
</template>

<script>
    export default {
        name: "SumOfInventoryTable",
        props: ['data'],
        data: () => ({
            currentSort: 'name',
            currentSortOrder: 'asc',
        }),
        methods: {
            customSort (value) {
                // Custom sort is used to fix issue with ignoring 0 valued records while sorting
                // See README.md and https://github.com/vuematerial/vue-material/issues/2087
                return value.sort((a, b) => {
                    const sortBy = this.currentSort

                    if (typeof(a[sortBy]) === "string")
                    {
                        if (this.currentSortOrder === 'desc') {
                            return a[sortBy].localeCompare(b[sortBy])
                        }

                        return b[sortBy].localeCompare(a[sortBy])
                    } else {
                        return this.currentSortOrder === 'desc'
                            ? (a[sortBy] > b[sortBy] ? 1 : -1)
                            : (a[sortBy] > b[sortBy] ? -1 : 1);
                    }
                })
            }
        }
    }
</script>

<style scoped>
</style>